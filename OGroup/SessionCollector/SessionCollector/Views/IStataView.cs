﻿using SessionCollector.BL.Entities;
using SessionCollector.BL.Entities.UI;
using System.Collections.Generic;

namespace SessionCollector.Views
{
	public interface IStataView
	{
		void SetData(IEnumerable<OSession> lst);
		void Go(IEnumerable<ChartItem> lst, string Title);
	}
}