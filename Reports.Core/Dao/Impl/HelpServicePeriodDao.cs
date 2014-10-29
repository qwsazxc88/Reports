using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpServicePeriodDao : DefaultDaoSorted<HelpServicePeriod>, IHelpServicePeriodDao
    {
        public HelpServicePeriodDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual List<HelpServicePeriod> LoadForPeriodSortedByOrder(int typeId)
        {
            int limitMonth = DateTime.Today.Year*100 + DateTime.Today.Month;
            return Session.Query<HelpServicePeriod>()
                .Where(x => x.Type.Id == typeId && x.PeriodMonth <= limitMonth)
                .OrderBy(a => a.SortOrder)
                .ToList();
        }
    }
}