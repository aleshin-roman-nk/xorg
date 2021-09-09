﻿using mvp_base;
using SessionCollector.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionCollector.Views
{
	public interface IDirectoriesView
	{
		event EventHandler<INode> RenameDirectory;
		event EventHandler<INode> DeleteDirectory;
		event EventHandler CreateDirectory;
		ViewResult<INode> Go();
		void DisplayNodes(IEnumerable<INode> lst, INode selectedNode = null);
	}
}