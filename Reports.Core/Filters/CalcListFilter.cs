using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Filters
{
	public class CalcListFilter : BaseListFilter
	{
		#region Fields
		private ReportType type;
		private int userId;
		#endregion

		#region Properties
		public ReportType Type
		{
			get { return type;}
			set { type = value;}
		}
		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}

		#endregion
		        #region Constructors

        public CalcListFilter()
        {
        }

		public CalcListFilter(ReportType type,int userId,
			string sortExpression, bool sortAscending, int maxResults, int firstResult)
        {
			this.type = type;
			this.userId = userId;
            SortExpression = sortExpression;
            SortAscending = sortAscending;
            MaxResults = maxResults;
            FirstResult = firstResult;
        }

        #endregion


	}
}
