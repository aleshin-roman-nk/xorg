﻿using Domain.DBContext;
using Domain.Entities;
using Domain.Repos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domain.Repos
{
	public class BufferTaskRepository : IBufferTaskRepository
	{
		IAppDataContextFactory _factory;
		ToolRepo _toolRepo;

		public BufferTaskRepository(IAppDataContextFactory f)
		{
			_factory = f;
			_toolRepo = new ToolRepo();
		}

		

		public BufferTask Create(int ftaskId)
		{
			using (var db = _factory.Create())
			{
				BufferTask res = new BufferTask { node_id = ftaskId };
				db.Entry(res).State = System.Data.Entity.EntityState.Added;
				db.SaveChanges();
				return res;
			}
		}

		public void Delete(int id)
		{
			using (var db = _factory.Create())
			{
				var i = db.BufferTasks.FirstOrDefault(x => x.id == id);
				if(i != null)
				{
					db.BufferTasks.Remove(i);
					db.SaveChanges();
				}
			}
		}

        public bool Exists(int taskId)
        {
			using (var db = _factory.Create())
            {
				var res = db.BufferTasks.SingleOrDefault(x => x.node_id == taskId);
				return res != null;
            }
		}

        public IEnumerable<BufferTask> GetAll()
		{
			using (var db = _factory.Create())
			{
				var res = db.BufferTasks.Include("Node").ToList();

				foreach (var item in res)
				{
					item.Node.path = _toolRepo.getFullPathOf(item.Node.id, db);
                }

				return res;
			}
		}
	}
}
