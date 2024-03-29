﻿using Domain.Entities;
using Shared.UI.Interfaces;
using Shared.UI.Interfaces.Enums;
using Shared.UI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBank.Presenters.EventDefinition;

namespace TaskBank.Views
{
	public interface IMainView: IStickable
	{
		INodesView NodesView { get; }
		event EventHandler StartDesriptionForm;
		event EventHandler StartCurrentBuffer;
		event EventHandler StartSessionCollector;
		event EventHandler StartWindowCompletedNodes;
		event EventHandler StartStatisticWindow;
		event EventHandler DeleteNode;
		event EventHandler CreateNode;
		event EventHandler RenameNode;
		event EventHandler OpenNode;
		event EventHandler CreateSession;
		event EventHandler RestoreWorkingSessionWindow;
		event EventHandler PutTaskToBuffer;
		event EventHandler<ApplicationClosingEventArgs> ApplicationClosing;
		event EventHandler<WorkingSessionPlayState> WorkingSessionPlayStateChanged;
		int OpenedTasksCout { get; set; }
		bool SessionState { get; set; }
		WorkingSessionPlayState SessionWorkingState { get; set; }
		int ClipboardNodesCount { get; set; }
	}
}
