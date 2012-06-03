using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.UI;

namespace Reports.Presenters.UI
{
	public class ReportsListPresenter:AbstractPresenter<IReportsListView>
	{
		#region Fields

		private IReportDao _reportDao;
		#endregion
		#region Properties

		public IReportDao ReportDao
		{
			get { return Validate.Dependency(_reportDao); }
			set { _reportDao = value; }
		}
		#endregion
	}
}
