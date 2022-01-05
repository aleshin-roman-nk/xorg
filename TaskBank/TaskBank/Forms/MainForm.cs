﻿using Domain.Entities;
using Shared.UI.UserControls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TaskBank.Forms;
using TaskBank.Views;

namespace TaskBank
{
	public partial class MainForm : Form, IMainView
	{
		public INodesView NodesView => nodesViewUC1;
		public Point LocationPoint => Location;

		public int width => Width;

		public int height => Height;

		int _openedTaskCount = 0;
		public int OpenedTasksCout
		{
			get
			{
				return _openedTaskCount;
			}
			set
			{
				_openedTaskCount = value;
				txtOpenedTaskCount.Text = _openedTaskCount.ToString();
			}
		}

		public MainForm()
		{
			InitializeComponent();

			setBufferState(0);
		}

		public event EventHandler StartDesriptionForm;
		public event EventHandler DeleteNode;
		public event EventHandler CreateNode;
		public event EventHandler RenameNode;
		public event EventHandler StartCurrentBuffer;
		public event EventHandler StartSessionCollector;
		public event EventHandler StartWindowCompletedNodes;

		void setBufferState(int items_cnt)
		{
			if (items_cnt == 0)
				txtBufferState.BackColor = Color.Gray;
			else
				txtBufferState.BackColor = Color.Yellow;

			txtBufferState.Text = $"{items_cnt}";
		}

		public void SetClipboardState(int cnt)
		{
			setBufferState(cnt);
		}
		//private void btnCreate_Click(object sender, EventArgs e)
		//{
		//	INTypeChooseView ntypeChooseForm = new NTypeChooseForm();

		//	var res = ntypeChooseForm.Choose(new List<NType> { NType.Note, NType.Task });

		//	if (res.Ok)
		//	{
		//		Create?.Invoke(this, res.Data);
		//	}
		//}

		//private void btnDelete_Click(object sender, EventArgs e)
		//{
		//	Delete?.Invoke(this, _currentNode);
		//}

		private void btnTimePoint_Click(object sender, EventArgs e)
		{
			var res = $">>> {DateTime.Now.ToString("dd-MM-yyyy HH:mm")}";
			try
			{
				Clipboard.Clear();
				Clipboard.SetText(res);
			}
			catch (Exception)
			{

			}
		}

		private void btnShowDescr_Click(object sender, EventArgs e)
		{
			StartDesriptionForm?.Invoke(this, EventArgs.Empty);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				DeleteNode?.Invoke(this, EventArgs.Empty);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DeleteNode?.Invoke(this, EventArgs.Empty);
		}

		private void btnRename_Click(object sender, EventArgs e)
		{
			RenameNode?.Invoke(this, EventArgs.Empty);
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			CreateNode?.Invoke(this, EventArgs.Empty);
		}

		private void btnShowCurentBuffer_Click(object sender, EventArgs e)
		{
			StartCurrentBuffer?.Invoke(this, EventArgs.Empty);
		}

		private void btnStartSessionCollector_Click(object sender, EventArgs e)
		{
			StartSessionCollector?.Invoke(this, EventArgs.Empty);
		}

		private void btnHideCompleted_Click(object sender, EventArgs e)
		{
			StartWindowCompletedNodes?.Invoke(this, EventArgs.Empty);
		}
	}
}