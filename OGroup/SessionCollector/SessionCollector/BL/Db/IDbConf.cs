﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionCollector.BL.Db
{
	public interface IDbConf
	{
		string DbSource { get; }
	}
}