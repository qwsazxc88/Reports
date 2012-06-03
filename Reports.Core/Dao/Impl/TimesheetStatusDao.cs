using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TimesheetStatusDao : DefaultDao<TimesheetStatus>, ITimesheetStatusDao
    {
        public TimesheetStatusDao(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }
        public IList<TimesheetStatus> LoadAllSorted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TimesheetStatus));
            criteria.AddOrder(new Order("ShortName", true));
            return criteria.List<TimesheetStatus>();
        }


    }
}