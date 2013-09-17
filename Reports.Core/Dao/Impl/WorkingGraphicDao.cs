using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class WorkingGraphicDao : DefaultDao<WorkingGraphic>, IWorkingGraphicDao
    {
        public WorkingGraphicDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IWorkingGraphicDao Members

        public IList<WorkingGraphic> LoadForIdsList(List<int> userIds,
                                                    int month, int year)
        {
            if(userIds.Count == 0)
                return new List<WorkingGraphic>();
            var beginDate = new DateTime(year, month, 1);
            DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
            ICriteria criteria = Session.CreateCriteria(typeof (WorkingGraphic));
            criteria.Add(Restrictions.In("UserId", userIds))
                .Add(Restrictions.Between("Day", beginDate, endDate));
            return criteria.List<WorkingGraphic>();
        }

        public IList<WorkingGraphic> LoadForIdsList(List<int> userIds,DateTime beginDate,DateTime endDate)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(WorkingGraphic));
            criteria.Add(Restrictions.In("UserId", userIds))
                .Add(Restrictions.Between("Day", beginDate, endDate));
            return criteria.List<WorkingGraphic>();
        }

        #endregion
    }
}