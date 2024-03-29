﻿using Domain.dto;
using Domain.Entities;
using Domain.Enums;
using Domain.Repos;
using Services.Nodes;
using SessionCollector;
using SessionCollector.Presenters;
using Shared.UI;
using Shared.UI.Dlg;
using Shared.UI.Forms;
using Shared.UI.Interfaces;
using Shared.UI.Interfaces.Enums;
using Shared.UI.Interfaces.EventArgsDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TaskBank.Presenters.EventDefinition;
using TaskBank.Views;
using Unity;
using xorg.Tools;

namespace TaskBank.Presenters
{
	public class TaskBankMainPresenter
	{
		List<NodeDTO> _clipboard = new List<NodeDTO>();

        IOpenObjectManager _openObjectManager;
		NodeInstanFactory _nodeInstanFactory;

        private readonly INodeService _service;
		private readonly IMainView _mainView;
		IDescriptionWindow _descView;

		IBufferTaskRepository _bufferTaskRepository;
		private readonly ISessionRepository sessionRepository;
		IBufferTaskView _currentTaskBufferView = null;

		ICompletedTasksView _completedTasksView = null;

		Func<IBufferTaskView> _bufferTaskViewFactory;
		Func<ICompletedTasksView> _completedTasksViewFactory;

		SessionManagerMainPresenter _sessionManagerMainPresenter = null;

		IInputBox _dialogs;

		TreeState _treeState;

        public TaskBankMainPresenter(IMainView mainView,
			IDescriptionWindow descriptionWindow,
            IOpenObjectManager openObjManager,
            NodeInstanFactory nodeInstanFactory,
			SessionManagerMainPresenter sessPres,
			IInputBox dlg,
			INodeService srv,
			Func<ICompletedTasksView> CompletedTasksViewFactory,

			Func<IBufferTaskView> BufferTaskViewFactory,
			IBufferTaskRepository bufferTaskRepository,
			ISessionRepository sessionRepository

			)
		{
			_service = srv;
			_mainView = mainView;
			_descView = descriptionWindow;
			_dialogs = dlg;
			_openObjectManager = openObjManager;
			_nodeInstanFactory = nodeInstanFactory;
            _sessionManagerMainPresenter = sessPres;
			_bufferTaskRepository = bufferTaskRepository;
			this.sessionRepository = sessionRepository;
			_bufferTaskViewFactory = BufferTaskViewFactory;
			_completedTasksViewFactory = CompletedTasksViewFactory;

			_mainView.NodesView.ActivateNode += _nodesView_ActivateNode;
			_mainView.NodesView.LeaveNode += _nodesView_LeaveNode;
			_mainView.NodesView.CurrentNodeChanged += NodesView_CurrentNodeChanged;
			_mainView.NodesView.Paste += NodesView_Paste;
			_mainView.NodesView.SendNodesToClipboard += NodesView_SendNodesToClipboard;

			_mainView.StartDesriptionForm += _mainView_StartDesriptionForm;
			_mainView.StartCurrentBuffer += _mainView_StartCurrentBuffer;
			_mainView.StartSessionCollector += _mainView_StartSessionCollector;
			_mainView.StartWindowCompletedNodes += _mainView_StartWindowCompletedNodes;
			_mainView.CreateNode += _mainView_CreateNode;
			_mainView.RenameNode += _mainView_RenameNode;
			_mainView.DeleteNode += _mainView_DeleteNode;

			_mainView.CreateSession += _mainView_CreateSession;
			_mainView.RestoreWorkingSessionWindow += _mainView_RestoreWorkingSessionWindow;
			_mainView.PutTaskToBuffer += _mainView_PutTaskToBuffer;
			_mainView.StartStatisticWindow += _mainView_StartStatisticWindow;
			_mainView.ApplicationClosing += _mainView_ApplicationClosing;
			_mainView.WorkingSessionPlayStateChanged += _mainView_WorkingSessionPlayStateChanged;
			_mainView.OpenNode += _mainView_OpenNode;

            //_descView.Save += DescView_Save;
            _descView.OpenNode += _descView_OpenNode;
            _descView.OpenById += _descView_OpenById;

            _service.CollectionChanged += _service_CollectionChanged;

			_openObjectManager.SaveNode += _openObjectManager_SaveNode;
            _openObjectManager.DeleteNodeTextPage += _openObjectManager_DeleteNodeTextPage;
            _openObjectManager.SaveSession += _openObjectManager_SaveSession;
			_openObjectManager.OpenTasksCountChanged += _openObjectManager_OpenTasksCountChanged;
			_openObjectManager.WorkingSessionWindowOpen += _openObjectManager_WorkingSessionWindowOpen;
			_openObjectManager.WorkingSessionWindowCompleted += _openObjectManager_WorkingSessionWindowCompleted;
			_openObjectManager.WorkingSessionPlayStateChanged += _openObjectManager_WorkingSessionPlayStateChanged;
			_openObjectManager.GetSessions += _openObjectManager_GetSessions;
			_openObjectManager.CreateSession += _openObjectManager_CreateSession;
			_openObjectManager.GetNode += _openObjectManager_GetNode;

			_sessionManagerMainPresenter.StartSession += _sessionManagerMainPresenter_StartSession;

			_nodeInstanFactory.AddNodeType<FTask>();
			_nodeInstanFactory.AddNodeType<Dir>(() =>
			{
				var str = _dialogs.Show("Enter dir name");
				if (!string.IsNullOrEmpty(str))
					return new Dir { name = str };
				else return null;
			});
			_nodeInstanFactory.AddNodeType<FNote>();

            update();
		}



        private void _openObjectManager_DeleteNodeTextPage(object sender, NodeTextPage e)
        {
			_service.DeleteNodeTextPage(e);
        }

        private void _mainView_OpenNode(object sender, EventArgs e)
		{
			var n = _mainView.NodesView.SelectedNodes.FirstOrDefault();
			if (n == null) return;
			if (n.type < NType.Dir) return;

			_openObjectManager.DefaultOpenNode(_service.GetNode(n.id));
		}

		private void _mainView_WorkingSessionPlayStateChanged(object sender, WorkingSessionPlayState e)
		{
			_openObjectManager.SetWorkingSessionPlayState(e);
		}

		private void _openObjectManager_WorkingSessionPlayStateChanged(object sender, WorkingSessionPlayState e)
		{
			_mainView.SessionWorkingState = e;

		}

		private void _openObjectManager_WorkingSessionWindowCompleted(object sender, EventArgs e)
		{
			_mainView.SessionState = false;
		}

		private void _openObjectManager_WorkingSessionWindowOpen(object sender, EventArgs e)
		{
			_mainView.SessionState = true;
		}

		private void _openObjectManager_GetNode(object sender, GetNodeEventArgs e)
		{
			var res = _service.GetNode(e.nodeId);

			if (res == null)
			{
				e.NodeExists = false;
			}
			else
			{
				e.NodeExists = true;
				e.Node = res as Node;
			}
		}

		private void _mainView_ApplicationClosing(object sender, ApplicationClosingEventArgs e)
		{
			e.AnyWorkingWindows = _openObjectManager.AnyWorkingWindow;
		}

		private void _openObjectManager_CreateSession(object sender, NodeDTO e)
		{
			_createSession(e);
		}

		private void _openObjectManager_GetSessions(object sender, GetSessionsEvenArgs e)
		{
			e.Sessions = _service.GetTopSessions(e.Date, e.taskId, e.itemsPerPage, e.page);
		}

		private void _mainView_StartStatisticWindow(object sender, EventArgs e)
		{
			_sessionManagerMainPresenter.ShowStataWindow(
				_mainView.NodesView.SelectedNodes.SingleOrDefault()
				);
		}

		private void _mainView_PutTaskToBuffer(object sender, EventArgs e)
		{
			var i = _mainView.NodesView.SelectedNodes.SingleOrDefault();

			if (i != null)
			{
				if (_bufferTaskRepository.Exists(i.id))
					_dialogs.ShowMessage($"Task [{i.path}{i.id}] already exists in the buffer");
				else
					_bufferTaskRepository.Create(i.id);
			}

			//if (dlgCurrentTaskBuffer != null)
			_currentTaskBufferView?.Update(_bufferTaskRepository.GetAll());
		}

		private void _mainView_RestoreWorkingSessionWindow(object sender, EventArgs e)
		{
			_openObjectManager.TryRestoreSessionWindow();

		}

		private void _sessionManagerMainPresenter_StartSession(object sender, OSession e)
		{
			_openObjectManager.OpenSession(e);

		}

		private void _openObjectManager_OpenTasksCountChanged(object sender, int e)
		{
			_mainView.OpenedTasksCout = e;
		}

		private void _openObjectManager_SaveSession(object sender, OSession e)
		{
			_sessionManagerMainPresenter.UpdateSession(e);
		}

		private void _openObjectManager_SaveNode(object sender, SaveNodeEventArgs e)
		{
			e.IsNodeSaved = _service.Update(e.Node) > 0;

			if (_completedTasksView != null)
				_completedTasksView.Display(
					_service.GetCompletedTasks(
						_completedTasksView.CurrentDate.Year,
						_completedTasksView.CurrentDate.Month));

			update();
		}

		private void _mainView_CreateSession(object sender, EventArgs e)
		{
			_createSession(_mainView.NodesView.SelectedNodes.SingleOrDefault());
		}

		private void _createSession(NodeDTO n)
		{
			/*
			 * >>> 04.10.2022
			 * Плохое решение куда то отправлять запрос
			 * Вполне можно здесь создать. Послать обновить окну коллектора сессий, если он открыт.
			 * А тут мы просто обращаемся к репозиторию сессий.
			 * Раз это центраный презентер.
			 */

			if (_sessionManagerMainPresenter.IsWindowRunning)
			{
				if (n != null)
				{
					//if (n is FTask)
					if (n.type >= NType.Dir)
					{
						//_sessionManagerMainPresenter.CreateSession((FTask)n);
						_sessionManagerMainPresenter.CreateSession(n);
					}
				}
			}
			else __tmpFuncCreateSessionIfNoSessionCollector(n);
		}

		private void __tmpFuncCreateSessionIfNoSessionCollector(NodeDTO n)
		{
			if (n != null)
			{
				//if (n is FTask)
				if (n.type >= NType.Dir)
				{
					//_sessionManagerMainPresenter.CreateSession((Node)n);

					var d = DateTime.Now;

					if (sessionRepository.ForNode(n.id).SessionExistsOnDate(d))
					{
						if (_dialogs.UserAnsweredYes($"Session of task you want to create is already created. Do you want to create a duplicate") == false)
							return;
					}

					//OSession session = new OSession { Owner = n as Node, NodeId = n.id };
					OSession session = new OSession();

					session.Start = d;
					session.ProvidedSeconds = 3600;

					sessionRepository.ForNode(n.id).Create(session);
				}
			}
		}

		private void NodesView_Paste(object sender, EventArgs e)
		{
			//_service.MoveNodesToDirectory(_service.CurrentOwner as Dir, _clipboard);
			_service.Move(_clipboard);

			_clipboard.Clear();
			_mainView.ClipboardNodesCount = _clipboard.Count();
		}

		private void NodesView_SendNodesToClipboard(object sender, IEnumerable<NodeDTO> e)
		{
			_clipboard.Clear();
			_clipboard.AddRange(e);

			_mainView.ClipboardNodesCount = _clipboard.Count();
		}

		private void _mainView_StartWindowCompletedNodes(object sender, EventArgs e)
		{
			if (_completedTasksView != null) return;

			_completedTasksView = _completedTasksViewFactory();
			_completedTasksView.DateChanged += _completedTasksView_DateChanged;
			_completedTasksView.OpenNode += _completedTasksView_OpenNode;
			_completedTasksView.Completed += _completedTasksView_Completed;

			var d = DateTime.Now;
			_completedTasksView.Display(_service.GetCompletedTasks(d.Year, d.Month));
		}

		private void _completedTasksView_Completed(object sender, EventArgs e)
		{
			_completedTasksView.DateChanged -= _completedTasksView_DateChanged;
			_completedTasksView.OpenNode -= _completedTasksView_OpenNode;
			_completedTasksView.Completed -= _completedTasksView_Completed;

			_completedTasksView = null;
		}

		private void _completedTasksView_OpenNode(object sender, INode e)
		{
			_openObjectManager.OpenTask(e as FTask);
		}

		private void _completedTasksView_DateChanged(object sender, DateTime e)
		{
			_completedTasksView.Display(_service.GetCompletedTasks(e.Year, e.Month));
		}

		private void _mainView_StartSessionCollector(object sender, EventArgs e)
		{
			_sessionManagerMainPresenter.ShowWindow();
		}

		private void _mainView_StartCurrentBuffer(object sender, EventArgs e)
		{
			if (_currentTaskBufferView != null) return;

			_currentTaskBufferView = _bufferTaskViewFactory();
			_currentTaskBufferView.Completed += _currentTaskBufferView_Completed;
			_currentTaskBufferView.CreateSession += _currentTaskBufferView_CreateSession;
			_currentTaskBufferView.Delete += _currentTaskBufferView_Delete;
			_currentTaskBufferView.Go(_bufferTaskRepository.GetAll());
		}

		private void _currentTaskBufferView_Delete(object sender, BufferTask e)
		{
			_bufferTaskRepository.Delete(e.id);
			_currentTaskBufferView.Update(_bufferTaskRepository.GetAll());
		}

		private void _currentTaskBufferView_CreateSession(object sender, NodeDTO e)
		{
			_createSession(e);
		}

		private void _currentTaskBufferView_Completed(object sender, EventArgs e)
		{
			_currentTaskBufferView.Completed -= _currentTaskBufferView_Completed;
			_currentTaskBufferView.CreateSession -= _currentTaskBufferView_CreateSession;
			_currentTaskBufferView.Delete -= _currentTaskBufferView_Delete;
			_currentTaskBufferView = null;
		}

		private void _mainView_DeleteNode(object sender, EventArgs e)
		{
			var i = _mainView.NodesView.SelectedNodes.FirstOrDefault();
			if (i.type < 0) return;

			if (_service.HasChildren(i))
			{
				_dialogs.ShowMessage($"[{i.path}] has CHILD NODES and could not be deleted.");
				return;
			}

			if (_service.HasSessions(i))
			{
				_dialogs.ShowMessage($"[{i.path}] has SESSIONS and could not be deleted.");
				return;
			}

			if (_dialogs.UserAnsweredYes($"Are you sure to kill `{i.name}` node"))
			{
				_service.Delete(i);
				update();
			}
		}

		private void _mainView_RenameNode(object sender, EventArgs e)
		{
			var i = _mainView.NodesView.SelectedNodes.FirstOrDefault();
			if (i.type < 0) return;

			var new_name = _dialogs.Show("Rename", i.name);

			if (string.IsNullOrEmpty(new_name) == false)
			{
				i.name = new_name;

				_service.UpdateName(i);
				update();
			}
		}

		private void _mainView_CreateNode(object sender, EventArgs e)
		{
			var res = _dialogs.ChooseNType(_nodeInstanFactory.Members);

			if (res.Ok)
			{
				INode n = null;

				n = _nodeInstanFactory.CreateNode(res.Data);

                if (n != null)
				{
					n.date = DateTime.Now;
					n.last_modified_date = DateTime.Now;

					var node = _service.Create(n);
					update();
					_mainView.NodesView.SetCursorAt(node);
				}
			}
		}

		private void _mainView_StartDesriptionForm(object sender, EventArgs e)
		{
			// в случае оптимизации, добавить в сервисы получить только строку, путь и id

			//var node = _service.GetNode(_mainView.NodesView.SelectedNodes.FirstOrDefault().id);

			_descView.Put(_mainView.NodesView.SelectedNodes.FirstOrDefault());
			_descView.StickTo(_mainView);
			_descView.Display();
		}

		private void NodesView_CurrentNodeChanged(object sender, NodeDTO e)
		{
			//var o = _service.GetNode(e.id);

			_descView.Put(e);
		}

        private void _descView_OpenNode(object sender, NodeDTO e)
        {
			_openNodeRouter(e);
        }
        private void _descView_OpenById(object sender, int e)
        {
            _openObjectManager.OpenNodeById(e);
        }
        private void DescView_Save(object sender, INode e)
		{
			if (e.type >= 0)
			{
				if (_openObjectManager.IsOpened(e.id))
				{
					_dialogs.ShowMessage($"[{e.path}] has already opened");
					return;
				}
				_service.Update(e);
			}
		}

		private void _service_CollectionChanged(object sender, EventArgs e)
		{
			update();
		}

		private void update()
		{
			// 13-09-2022 Грузим все ноды, в сетке помечаем, которые закрыты
			_mainView.NodesView.DisplayNodes(
				_service.Items,
				_service.CurrentParentFullName,
				_service.HighlightedNode);
		}

		private void _nodesView_LeaveNode(object sender, EventArgs e)
		{
			_service.JumpBack();
		}

		private void _nodesView_ActivateNode(object sender, NodeDTO e)
		{
			_openNodeRouter(e);
        }

		private void _openNodeRouter(NodeDTO node)
		{
            if (node.type == NType.Dir || node.type == NType.exit_dir)
            {
                _service.Enter(node);
            }
            else if (node.type == NType.Task)
            {
                var tsk = _service.GetNode(node.id);
                _openObjectManager.OpenTask(tsk as FTask);
            }
            else
            {
                _openObjectManager.DefaultOpenNode(_service.GetNode(node.id));
            }
        }

		//==== tree state manager

		//private void leaveDir()
		//{
  //          _treeState.NodeUnderCursor = _treeState.Parents.Pop();
  //          _treeState.Children = _service.GetChildrenOf(_treeState.Parents.Peek());
  //          _mainView.NodesView.RenderData(_treeState.Children, _treeState.Parents, _treeState.NodeUnderCursor);
  //      }

		//private void enterDir(INode p)
		//{
		//	_treeState.Parents.Push(p);
		//	_treeState.Children = _service.GetChildrenOf(p);
		//	_treeState.NodeUnderCursor = null;
		//	_mainView.NodesView.RenderData(_treeState.Children, _treeState.Parents, _treeState.NodeUnderCursor);
  //      }
    }
}
