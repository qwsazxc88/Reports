using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
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
        protected IUserDao userDao;
        public ITimesheetStatusDao TimesheetStatusDao
        {
            get { return Validate.Dependency(timesheetStatusDao); }
            set { timesheetStatusDao = value; }
        }
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
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

        protected IList<RequestDto> GetRequestsForType(DateTime beginDate,DateTime endDate,RequestTypeEnum type,
            int userId,UserRole userRole)
        {
            string sqlQuery =
                @"select v.BeginDate as BeginDate,
                         v.EndDate as EndDate,
                         v.TimesheetStatusId  as TimesheetStatusId,
                         ts.[ShortName] as TimesheetCode,
                         null as TimesheetHours,
                         u.Id as UserId,
                         u.Name as UserName";
            switch(type)
            {
                case RequestTypeEnum.Vacation:
                    sqlQuery += @" from [dbo].[Vacation] v ";
                    break;
                case RequestTypeEnum.Absence:
                    sqlQuery += @" from [dbo].[Absence] v ";
                    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестный тип заявки {0}", type));

            }
            sqlQuery += @" inner join [dbo].[TimesheetStatus] ts on ts.Id = v.TimesheetStatusId
                           inner join [dbo].[Users] u on u.Id = v.UserId ";
            sqlQuery+= @" where ((v.BeginDate between :beginDate and :endDate) or
                                 (v.EndDate between :beginDate and :endDate) or 
                                 (:beginDate between v.BeginDate and v.EndDate) or
                                 (:endDate between v.BeginDate and v.EndDate))
                          and v.DeleteDate is null 
                          and v.UserDateAccept is not  null
                          and v.ManagerDateAccept is not null
                          and v.PersonnelManagerDateAccept is not null ";
            switch(userRole)
            {
                case UserRole.Employee:
                    sqlQuery += " and v.UserId = :userId ";
                    break;
                case UserRole.Manager:
                    sqlQuery += " and u.ManagerId = :userId ";
                    break;
                case UserRole.PersonnelManager:
                    sqlQuery += " and u.PersonnelManagerId = :userId ";
                    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}",userRole));

            }
            sqlQuery += " order by UserName,UserId,BeginDate,EndDate ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
               AddScalar("BeginDate", NHibernateUtil.DateTime).
               AddScalar("EndDate", NHibernateUtil.DateTime).
               AddScalar("TimesheetStatusId", NHibernateUtil.Int32).
               AddScalar("TimesheetCode", NHibernateUtil.String).
               AddScalar("TimesheetHours", NHibernateUtil.Int32).
               AddScalar("UserId", NHibernateUtil.Int32).
               AddScalar("UserName", NHibernateUtil.String).
               SetDateTime("beginDate", beginDate).
               SetDateTime("endDate", endDate).
               SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(RequestDto))).List<RequestDto>();
        }
        protected IList<RequestDto> GetRequests(DateTime beginDate,DateTime endDate,int userId,UserRole userRole)
        {
            List<RequestDto> allList = new List<RequestDto>();
            allList.AddRange(GetRequestsForType(beginDate, endDate, RequestTypeEnum.Vacation, userId, userRole));
            allList.AddRange(GetRequestsForType(beginDate, endDate, RequestTypeEnum.Absence, userId, userRole));
            return allList;
        }
        public IList<DayRequestsDto> GetRequestsForMonth(int month,int year,int userId,UserRole userRole)
        {
            DateTime current = new DateTime(year,month, 1);
            IList<DayRequestsDto> dtoList = new List<DayRequestsDto>();
            for (int i = 0; i < 31; i++)
            {
                DayRequestsDto dto = new DayRequestsDto {Day = current};
                if (current.Month != month)
                    break;
                dtoList.Add(dto);
                current = current.AddDays(1);
            }
            IList<RequestDto> requests = GetRequests(dtoList.First().Day, dtoList.Last().Day, userId, userRole);
            IList<IdNameDto> users;
            switch(userRole)
            {
                case UserRole.Employee:
                    users = new List<IdNameDto>{ new IdNameDto(userId,UserDao.Load(userId).FullName)};
                    break;
                case UserRole.Manager:
                case UserRole.PersonnelManager:
                    users = UserDao.GetUsersForManager(userId, userRole); 
                    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}",userRole));
            }
            foreach (var idNameDto in users)
            {
                foreach (var dayRequestsDto in dtoList)
                {
                    DateTime date = dayRequestsDto.Day;
                    List<RequestDto> userRequestList = requests.Where(x => 
                        (date >= x.BeginDate && date <= x.EndDate && idNameDto.Id == x.UserId)).ToList();
                    if (userRequestList.Count == 0)
                    {
                        DayOfWeek dayOfweek = date.DayOfWeek;
                        bool isHoliday = (dayOfweek == DayOfWeek.Sunday) || (dayOfweek == DayOfWeek.Saturday);
                        dayRequestsDto.Requests.Add(new RequestDto
                                                        {
                                                            BeginDate = date,
                                                            EndDate = date,
                                                            TimesheetCode =
                                                                isHoliday ?HolidayStatusCode: PresenceStatusCode,
                                                            TimesheetHours = isHoliday ? new int?() : 8,
                                                            UserId = idNameDto.Id,
                                                            UserName = idNameDto.Name
                                                        });
                    }
                    dayRequestsDto.Requests.AddRange(userRequestList);
                }
            }
            return dtoList;
        }
    }
}