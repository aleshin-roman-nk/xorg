﻿using Domain.dto;
using Domain.Entities;
using Shared.UI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskBank.Views
{
	public interface IDescriptionWindow
	{
		void Put(NodeDTO n);
		//event EventHandler<INode> Save;
		void Display();
		void StickTo(IStickable to);
		event EventHandler<NodeDTO> OpenNode;
		event EventHandler<int> OpenById;
	}
}
