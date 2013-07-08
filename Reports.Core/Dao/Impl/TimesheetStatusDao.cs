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
        public virtual TimesheetStatus GetStatusForShortName(string  shortName)
        {
            return (TimesheetStatus)Session.CreateCriteria(typeof(TimesheetStatus))
                  .Add(Restrictions.Eq("ShortName", shortName))
                //                  .Add(Restrictions.Eq("IsActive", true))
                  .UniqueResult();
        }
        //public IList<TimesheetStatus> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(TimesheetStatus));
        //    criteria.AddOrder(new Order("ShortName", true));
        //    return criteria.List<TimesheetStatus>();
        //}


    }
}