﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoriesWolking.Models.DirNavigator
{
	public interface INode
	{
		int id { get; set; }
		int owner_id { get; set; }
		string name { get; set; }
		NType type { get; set; }
	}
}