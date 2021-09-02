﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace Domain.DBContexts
{
	public class MainContext: DbContext
	{
		public MainContext(string cn) : base(cn)
		{

		}

		public DbSet<Node> Nodes { get; set; }
		public DbSet<FNote> Notes { get; set; }
		public DbSet<Dir> Dirs { get; set; }
		public DbSet<FTask> Tasks { get; set; }
	}
}
