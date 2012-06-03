using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TimesheetDao : DefaultDao<Timesheet>, ITimesheetDao
    {
        public const string PresenceStatusCode = "Я";
        public const string HolidayStatusCode = "В";
        public const float PresenceHours = 8;
        public const float HolidayHours = 0;

        private const string UserAlias = "User";

        protected ITimesheetStatusDao timesheetStatusDao;
        public ITimesheetStatusDao TimesheetStatusDao
        {
            get { return Validate.Dependency(timesheetStatusDao); }
            set { timesheetStatusDao = value; }
        }


        public TimesheetDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        //public virtual Timesheet FindByUserAndMonth(int userId,DateTime month)
        //{
        //    return (Timesheet)Session.CreateCriteria(typeof(Timesheet))
        //                      .Add(Restrictions.Eq("User.Id", userId))
        //                      .Add(Restrictions.Eq("Month", userId))
        //                      .UniqueResult();
        //}

        public IList<DateTime> GetTimesheetDates()
        {
            string sqlQuery =
                (@"select distinct(Month)
                        from dbo.Timesheet ts 
                        inner join dbo.Users u on u.Id = ts.UserId ");
            ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
            return query.List<DateTime>();
        }

        public IList<DateTime> GetTimesheetDatesForManager(int managerId,UserRole role)
        {
            string sqlQuery =
                (@"select distinct(Month)
                        from dbo.Timesheet ts 
                        inner join dbo.Users u on u.Id = ts.UserId ");
            switch (role)
            {
                case UserRole.Manager:
                    sqlQuery += @"where u.ManagerId = :managerId";
                    break;
                case UserRole.PersonnelManager:
                    sqlQuery += @"where u.PersonnelManagerId = :managerId";
                    break;
            }
            ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
            query.SetInt32("managerId", managerId);
            return query.List<DateTime>();
        }
        public void UpdateTimesheetDays(IEnumerable<int> timesheetIds,int statusId,
            float hours,int number)
        {
            if (timesheetIds.Count() == 0)
                return;
            string timesheetIdsIn = timesheetIds.Aggregate(string.Empty, (current, x) => current + x.ToString() + ",");
            timesheetIdsIn = timesheetIdsIn.Substring(0, timesheetIdsIn.Length - 1);
            int updateCount = Session.CreateSQLQuery
                                    (@"update TimesheetDay set StatusId=:statusId,
                                        Hours=:hours
                                        where Number = :number
                                        and TimesheetId in ("+timesheetIdsIn+")").
                                    SetInt32("statusId", statusId).
                                    SetDouble("hours", hours).
                                    SetInt32("number", number).
//                                    SetString("timesheetIds", timesheetIdsIn).
                                    ExecuteUpdate();
            Log.DebugFormat("{0} timesheet days updated.",updateCount);
        }
        public virtual IList<Timesheet> LoadTimesheetsForMonth(DateTime date)
        {
            return Session.CreateCriteria(typeof(Timesheet))
            .Add(Restrictions.Eq("Month", date))
            .List<Timesheet>();
        }
        public IList<TimesheetDaysDto> LoadTimesheetsForMonth(int managerId, DateTime month,UserRole role)
        {
            string sqlQuery =
                (@"select  td.*
		            ,tss.ShortName as Status 
	                ,ts.[Month]
		            ,u.Id as UserId
		            ,u.Name 
		            ,u.Code
                    from dbo.TimesheetDay td
                    inner join dbo.Timesheet ts on ts.id = td.TimesheetId
                    inner join dbo.Users u on u.Id = ts.UserId
                    inner join dbo.TimesheetStatus tss on tss.id = td.StatusId
                    where ts.[Month] = :month
                    and ((u.DateRelease is null) or (u.DateRelease > :month)) 
                    "
                );
                switch (role)
                {
                    case UserRole.Manager:
                        sqlQuery += @"and u.ManagerId = :managerId";
                        break;
                    case UserRole.PersonnelManager:
                        sqlQuery += @"and u.PersonnelManagerId = :managerId";
                        break;
                }
                return Session.CreateSQLQuery(sqlQuery).
                SetInt32("managerId",managerId).
                SetDateTime("month", month).
                SetResultTransformer(Transformers.AliasToBean(typeof(TimesheetDaysDto))).
                List<TimesheetDaysDto>().OrderBy(x =>x.Name).ToList();
        }
        public void CheckAndCreateTimesheetsForMonth(int managerId,DateTime month)
        {
            try
            {
                IList<int> userWithoutTimesheetIds =  Session.CreateSQLQuery
                (@"select u.Id from users u
                   where u.ManagerId = :managerId
                   and not exists 
                   (
	                select * from dbo.Timesheet ts
	                where Month = :month
	                and ts.UserId = u.Id
                    )"
                 ).
                 AddScalar("Id", NHibernateUtil.Int32).
                 SetInt32("managerId",managerId).
                 SetDateTime("month", month).
                 /*SetResultTransformer(Transformers.AliasToBean(typeof(int))).*/List<int>();

                IList<User> userWithoutTimesheet = Session.
                    CreateCriteria(typeof (User), UserAlias).
                    Add(Restrictions.In("Id", userWithoutTimesheetIds.ToArray())).
                    List<User>();
                    
                   
                IList<TimesheetStatus> statuses = TimesheetStatusDao.LoadAllSorted();
                foreach (User user in userWithoutTimesheet)
                {
                    Log.DebugFormat("Begin create timesheet for user {0} and month {1}", user.Id, month);
                    if(!user.DateRelease.HasValue || (user.DateRelease.Value < month.AddMonths(1)))
                        CreateTimesheet(user, month, statuses);
                    Log.DebugFormat("End create timesheet for user {0} and month {1}", user.Id, month);
                }
                Flush();
                Log.Debug("After fhush (CheckAndCreateTimesheetsForMonth)");
            }
            catch (Exception ex)
            {
                Log.Error("Exception",ex);
                throw;
            }
        }
        protected Timesheet CreateTimesheet(User user,DateTime month,IList<TimesheetStatus> statuses)
        {
            
            Timesheet timesheet = new Timesheet {User = user,Month = month,Days = new List<TimesheetDay>()};

            DateTime current = new DateTime(month.Year, month.Month,1);
            for(int i=0;i<31;i++)
            {
                DayOfWeek dayOfweek = current.DayOfWeek;
                bool isHoliday = (dayOfweek == DayOfWeek.Sunday) || (dayOfweek == DayOfWeek.Saturday);
                if (current.Month != month.Month)
                    break;
                TimesheetStatus status = isHoliday
                                    ? statuses.Where(x => x.ShortName == HolidayStatusCode).First()
                                    : statuses.Where(x => x.ShortName == PresenceStatusCode).First();
                float hours = isHoliday ? HolidayHours :PresenceHours;

                TimesheetDay day = new TimesheetDay
                                        {
                                            Number = i+1,
                                            Timesheet = timesheet,
                                            Hours = hours,
                                            Status = status,
                                        };
                timesheet.Days.Add(day);
                current = current.AddDays(1);
            }
            return Merge(timesheet);
        }
        public IList<Timesheet> GetTimesheetListForOwner(int ownerId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(Timesheet));
            criteria.Add(Restrictions.Eq("User.Id", ownerId));
            criteria.AddOrder(new Order("Month", false));
            return criteria.List<Timesheet>();
        }
    }
}