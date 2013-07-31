using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class WorkingCalendarDao : DefaultDao<WorkingCalendar>, IWorkingCalendarDao
    {
        public WorkingCalendarDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public virtual IList<WorkingCalendar> GetEntitiesBetweenDates(int month, int year)
        {
            DateTime beginDate = new DateTime(year,month,1);
            DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
            return Session.CreateCriteria(typeof(WorkingCalendar))
                  .Add(Restrictions.Between("Date",beginDate,endDate))
                    .List<WorkingCalendar>();
        }
        public virtual IList<WorkingCalendar> GetEntitiesBetweenDates(DateTime beginDate,DateTime endDate)
        {
            return Session.CreateCriteria(typeof(WorkingCalendar))
                  .Add(Restrictions.Between("Date", beginDate, endDate))
                    .List<WorkingCalendar>();
        }
      
    }
}