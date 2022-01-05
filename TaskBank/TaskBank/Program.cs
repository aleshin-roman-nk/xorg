﻿using Domain.DBContext;
using Domain.Repos;
using Services.Nodes;
using Services.Sessions;
using SessionCollector;
using SessionCollector.Forms;
using SessionCollector.Views;
using Shared.UI;
using Shared.UI.Dlg;
using Shared.UI.Forms;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TaskBank.Forms;
using TaskBank.Presenters;
using TaskBank.Views;
using Unity;
using xorg.Tools;

namespace TaskBank
{
	static class Program
	{
		[DllImport("user32.dll")]
		private static extern Boolean ShowWindow(IntPtr hWnd, uint nCmdShow);

		[DllImport("user32.dll")]
		private static extern int SetForegroundWindow(IntPtr hwnd);

		private const uint SW_RESTORE = 0x09;

		private static string appGuid = "AF015778-EEE7-4EE9-B52B-A96A5DE7311B";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
			{
				if (!mutex.WaitOne(0, false))
				{
					MessageBox.Show("Task bank is already running");
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				IUnityContainer container = new UnityContainer();

				//Debugger.init();
				Logger.Clear();

				IMainView mf = new MainForm();

				container

					.RegisterType<ISCMainView, SCMainForm>()
					.RegisterType<ISingleSessionView, SessionForm>()
					//.RegisterType<IBufferTaskView, CurrentTaskBufferForm>()
					.RegisterType<ISessionService, SessionService>()
					//.RegisterType<ISessionRepository, SessionRepository>()
					//.RegisterType<IAppDataContextFactory, AppDataContextFactory>()
					//.RegisterInstance(dbConf)
					.RegisterType<IStataView, StataForm>()

					.RegisterInstance(mf)
					.RegisterType<IAppDataContextFactory, AppDataContextFactory>()
					.RegisterType<IDescriptionWindow, DescriptionForm>()
					.RegisterType<IInputBox, InputBox>()
					.RegisterType<IDbConf, DbConf>()
					.RegisterType<INodeService, NodeService>()
					.RegisterType<INodeRepository, NodeRepository>()
					.RegisterType<IBufferTaskRepository, BufferTaskRepository>()
					.RegisterType<IFTaskEditView, FTaskForm>()
					.RegisterType<IBufferTaskView, CurrentTaskBufferForm>()
					.RegisterType<ISessionRepository, SessionRepository>()
					.RegisterInstance(container);

				var presenter = container.Resolve<TaskBankMainPresenter>();

				Application.Run((Form)mf);
			}
		}
	}
}