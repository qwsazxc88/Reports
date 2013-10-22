using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TerraGraphicDao : DefaultDao<TerraGraphic>, ITerraGraphicDao
    {
        public TerraGraphicDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<TerraGraphic> LoadForIdsList(List<int> userIds,
                                                   int month, int year)
        {
            if (userIds.Count == 0)
                return new List<TerraGraphic>();
            var beginDate = new DateTime(year, month, 1);
            DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
            ICriteria criteria = Session.CreateCriteria(typeof(WorkingGraphic));
            criteria.Add(Restrictions.In("UserId", userIds))
                .Add(Restrictions.Between("Day", beginDate, endDate));
            return criteria.List<TerraGraphic>();
        }
    }
}