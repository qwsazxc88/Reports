using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class WorkingDaysConstantDao : DefaultDao<WorkingDaysConstant>, IWorkingDaysConstantDao
    {
        public WorkingDaysConstantDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public List<WorkingDaysConstant> LoadDataForYear(int year)
        {
            return Session.CreateCriteria(typeof(WorkingDaysConstant))
                   .Add(Restrictions.Between("Month", new DateTime(year,1,1), 
                                             new DateTime(year,12,31)))
                   .AddOrder(new Order("Month", true))
                   .List<WorkingDaysConstant>().ToList();
        }
        public WorkingDaysConstant LoadDataForMonth(int month,int year)
        {
            return Session.CreateCriteria(typeof(WorkingDaysConstant))
                   .Add(Restrictions.Eq("Month", new DateTime(year, month, 1)))
                   .List<WorkingDaysConstant>().FirstOrDefault();
        }
        public List<WorkingDaysConstant> LoadDataForDates(DateTime beginDate,DateTime endDate)
        {
            return Session.CreateCriteria(typeof(WorkingDaysConstant))
                   .Add(Restrictions.Between("Month", new DateTime(beginDate.Year, beginDate.Month, beginDate.Day),
                                             new DateTime(endDate.Year, endDate.Month, endDate.Day)))
                   .AddOrder(new Order("Month", true))
                   .List<WorkingDaysConstant>().ToList();
        }
    }
}