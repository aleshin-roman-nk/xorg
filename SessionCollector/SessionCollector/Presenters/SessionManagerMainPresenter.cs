﻿using Domain.dto;
using Domain.Entities;
using Services.Sessions;
using SessionCollector.Views;
using Shared.UI;
using Shared.UI.Interfaces;
using System;
using xorg.Tools;

namespace SessionCollector.Presenters
{
	public class SessionManagerMainPresenter
	{
		ISCMainView _view = null;
		Func<ISCMainView> _createMainViewFactory;
		IInputBox _dialogs;

		ISessionService _sessionService;

		Func<IStataView> _stataViewFactory;
		IStataView _stataView = null;

		public event EventHandler<OSession> StartSession;
		//public event EventHandler<ExtendSessionTomorrowEventArgs> ExtendSessionTomorrow;

		public bool IsWindowRunning => _view != null;
		public void ShowWindow()
		{
			if(_view == null)
            {
				_view = _createMainViewFactory();

				_view.DateChanged += _view_DateChanged;
				_view.StartSession += _view_StartSession;
				_view.DeleteSession += _view_DeleteSession;
				_view.KickNextDay += _view_KickNextDay;
				_view.KickPrevDay += _view_KickPrevDay;
				_view.WindowClosed += _view_WindowClosed;
                _view.ExtendSessionTomorrow += _view_ExtendSessionTomorrow;
			}

			_view.ShowWindow();
		}

		private void _view_WindowClosed(object sender, EventArgs e)
		{
			_view.DateChanged -= _view_DateChanged;
			_view.StartSession -= _view_StartSession;
			_view.DeleteSession -= _view_DeleteSession;
			_view.KickNextDay -= _view_KickNextDay;
			_view.KickPrevDay -= _view_KickPrevDay;
			_view.WindowClosed -= _view_WindowClosed;
			_view.ExtendSessionTomorrow -= _view_ExtendSessionTomorrow;

			_view = null;
		}

		private void _view_ExtendSessionTomorrow(object sender, OSession e)
        {
			var d = _view.CurrentDateTime.AddDays(1);

			if (_creatingAllowed(e.NodeId, d) == false) return;

			//ExtendSessionTomorrow?.Invoke(sender, new ExtendSessionTomorrowEventArgs(e.NodeId, _view.CurrentDateTime));
			OSession s = new OSession { NodeId = e.NodeId, Start = d, ProvidedSeconds = 3600 };
			//_sessionService.Repo.Update(s);
			_sessionService.Repo.ForNode(e.NodeId).Create(s);

            _dialogs.ShowMessage("Session has successfully created");
		}
        public void ShowStataWindow(NodeDTO n)
        {
			if (n == null) return;

			if(_stataView == null)
            {
				_stataView = _stataViewFactory();

				_stataView.Completed += _stataView_Completed;
                _stataView.DateChanged += _stataView_DateChanged;

				_stataView.Go(n);
			}
		}

        private void _stataView_DateChanged(object sender, EventArgs e)
        {
			var i = _sessionService.GetStatistic(
				_stataView.CurrentDate.Year,
				_stataView.CurrentDate.Month,
				new NodeDTO { id = _stataView.Node.id });

			_stataView.Display(i, $"{_stataView.Node.path}");
		}

        private void _stataView_Completed(object sender, EventArgs e)
        {
			_stataView.Completed += _stataView_Completed;
			_stataView.DateChanged += _stataView_DateChanged;

			_stataView = null;
		}

        public void CreateSession(NodeDTO t)
		{
			if (_creatingAllowed(t.id, _view.CurrentDateTime) == false) return;

			_sessionService.Repo
				.ForNode(t.id)
				.Create(new OSession { Start = _view.CurrentDateTime, ProvidedSeconds = 3600});

			displaySessions(_view.CurrentDateTime);
		}

		bool _creatingAllowed(int nodeid, DateTime d)
        {
			if(_sessionService.Repo.ForNode(nodeid).SessionExistsOnDate(d))
            {
				return _dialogs.UserAnsweredYes($"Session of task you want to create is already created. Do you want to create a duplicate");
            }

			return true;
        }

		public void UpdateSession(OSession s)
        {
			_sessionService.Repo.Update(s);
			if(_view != null)
				displaySessions(_view.CurrentDateTime);
		}



		public SessionManagerMainPresenter(
			Func<ISCMainView> SCMainViewFactory,
			Func<IStataView> stataViewFactiry,

			IInputBox dlg,
			ISessionService s)
		{
			_createMainViewFactory = SCMainViewFactory;
			_stataViewFactory = stataViewFactiry;
			_sessionService = s;
			_dialogs = dlg;
		}



		private void _view_KickPrevDay(object sender, OSession e)
		{
			DateTime d = e.Start.AddDays(-1);
			if (_creatingAllowed(e.NodeId, d) == false) return;

			if (e == null) return;

			e.Start = d;
			_sessionService.Repo.Update(e);
			displaySessions(_view.CurrentDateTime);
		}

		private void _view_KickNextDay(object sender, OSession e)
		{
			DateTime d = e.Start.AddDays(1);
			if (_creatingAllowed(e.NodeId, d) == false) return;

			if (e == null) return;

			e.Start = d;
			_sessionService.Repo.Update(e);
			displaySessions(_view.CurrentDateTime);
		}

		private void _view_DeleteSession(object sender, OSession e)
		{
			if (_dialogs.UserAnsweredYes($"Session will be killed at all\n{e.Description}"))
			{
				_sessionService.Repo.Delete(e);
				displaySessions(_view.CurrentDateTime);
			}
		}

		private void _view_StartSession(object sender, OSession e)
		{
			StartSession?.Invoke(this, e);
		}

		private void displaySessions(DateTime d)
		{
			_sessionService.SetCollectionOfDate(d);

			_view.DisplaySessions(
				_sessionService.Items, 
				_sessionService.AllocatedHours,
				_sessionService.DoneWorkInSeconds,
				_sessionService.LastSessionFinish);
		}

		private void _view_DateChanged(object sender, DateTime e)
		{
			displaySessions(e);
		}
	}
}
