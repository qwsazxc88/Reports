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
using Reports.Core.Utils;

namespace Reports.Core.Dao.Impl
{
    public class TimesheetDao : DefaultDao<Timesheet>, ITimesheetDao
    {
        public const string PresenceStatusCode = "Я";
        public const string HolidayStatusCode = "В";
        public const string EmptyStatusCode = " ";

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
            const string sqlQuery = (@"select distinct(Month)
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
                    sqlQuery += @" and exists ( select * from UserToPersonnel up where up.PersonnelId = :managerId and u.Id = up.UserId ) ";//@"where u.PersonnelManagerId = :managerId";
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
                        sqlQuery += @" and exists ( select * from UserToPersonnel up where up.PersonnelId = :managerId and u.Id = up.UserId ) ";//@"and u.PersonnelManagerId = :managerId";
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
        protected IList<RequestDto> GetRequestsForTypeOneDay(DateTime beginDate, DateTime endDate, RequestTypeEnum type,
            int userId, UserRole userRole, IList<int> userIds)
        {
            string sqlQuery =
                string.Format(@"select v.{0} as BeginDate,
                         v.{0} as EndDate,
                         v.TimesheetStatusId  as TimesheetStatusId,
                         ts.[ShortName] as TimesheetCode,
                         v.Hours as TimesheetHours,
                         u.Id as UserId,
                         u.Name as UserName", type == RequestTypeEnum.HolidayWork ? "WorkDate" : "EventDate");
            switch (type)
            {
                case RequestTypeEnum.HolidayWork:
                    sqlQuery += @" from [dbo].[HolidayWork] v ";
                    break;
                case RequestTypeEnum.TimesheetCorrection:
                    sqlQuery += @" from [dbo].[TimesheetCorrection] v ";
                    break;
                //case RequestTypeEnum.Sicklist:
                //    sqlQuery += @" from [dbo].[Sicklist] v ";
                //    break;
                //case RequestTypeEnum.Mission:
                //    sqlQuery += @" from [dbo].[Mission] v ";
                //    break;

                default:
                    throw new ArgumentException(string.Format("Неизвестный тип заявки {0}", type));

            }
            sqlQuery += @" inner join [dbo].[TimesheetStatus] ts on ts.Id = v.TimesheetStatusId
                           inner join [dbo].[Users] u on u.Id = v.UserId ";
            sqlQuery += string.Format(@" where 
                          v.{0} between :beginDate and :endDate
                          and v.DeleteDate is null 
                          and v.UserDateAccept is not  null
                          and v.ManagerDateAccept is not null
                          and v.PersonnelManagerDateAccept is not null ",
                          type == RequestTypeEnum.HolidayWork ? "WorkDate" : "EventDate");
            sqlQuery += " and u.Id in (:userList)";
            //switch (userRole)
            //{
            //    case UserRole.Employee:
            //        sqlQuery += " and v.UserId = :userId ";
            //        break;
            //    case UserRole.Manager:
            //        sqlQuery += " and u.ManagerId = :userId ";
            //        break;
            //    case UserRole.PersonnelManager:
            //        sqlQuery += " and exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ";//" and u.PersonnelManagerId = :userId ";
            //        break;
            //    default:
            //        throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));

            //}
            sqlQuery += string.Format(" order by UserName,UserId,{0}",
                type == RequestTypeEnum.HolidayWork ? "WorkDate" : "EventDate");
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
               SetParameterList("userList", userIds);
               //SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(RequestDto))).List<RequestDto>();
        }
        protected IList<RequestDto> GetRequestsForDismissalAndDaysAfter(DateTime beginDate, DateTime endDate,
                int managerId, UserRole managerRole, IList<int> userIds)
        {
            List<RequestDto> list = GetDismissalUsers(beginDate, endDate, managerId, managerRole, userIds).ToList();
            IList<RequestDto> requestList = GetRequestsForDismissal(beginDate, endDate, managerId, managerRole,userIds);
            foreach (RequestDto requestDto in requestList)
            {
                    List<RequestDto> otherUser = list.Where(x => x.UserId == requestDto.UserId).ToList();
                    if (otherUser.Count == 0)
                    {
                        requestDto.IsDismissalDay = true;
                        list.Add(requestDto);
                        break;
                    }
            }
            foreach (RequestDto requestDto in requestList)
            {
                RequestDto dto =
                    list.Where(x => x.UserId == requestDto.UserId && x.BeginDate > requestDto.BeginDate).FirstOrDefault();
                if (dto != null)
                {
                    dto.BeginDate = requestDto.BeginDate;
                    dto.EndDate = requestDto.EndDate;
                    break;
                }
            }
            //foreach (RequestDto requestDto in list)
            //{
            //    DayOfWeek dayOfweek = requestDto.BeginDate.DayOfWeek;
            //    if ((dayOfweek == DayOfWeek.Sunday) || (dayOfweek == DayOfWeek.Saturday))
            //    {
            //        requestDto.TimesheetCode = HolidayStatusCode;
            //        requestDto.TimesheetHours = new int?();
            //        requestDto.TimesheetStatusId = 0;
            //    }
            //}
            //foreach (RequestDto requestDto in list.Where(requestDto => requestDto.TimesheetCode == PresenceStatusCode))
            //    requestDto.TimesheetHours = 8;
            IList<RequestDto> afterList = list.Where(x => x.EndDate < endDate).Select(requestDto => new RequestDto
            {
                BeginDate = requestDto.EndDate.AddDays(1),
                EndDate = endDate,
                TimesheetCode = EmptyStatusCode,
                TimesheetHours = new int?(),
                TimesheetStatusId = 0,
                UserId = requestDto.UserId,
                UserName = requestDto.UserName,
            }).ToList();
            List<RequestDto> result = list.ToList();
            result.AddRange(afterList);
            return result;
        }
        protected IList<RequestDto> GetDismissalUsers(DateTime beginDate, DateTime endDate,
                    int managerId, UserRole managerRole, IList<int> userIds)
        {
            string sqlQuery =
                string.Format(@"select u.DateRelease as BeginDate,
                         u.DateRelease as EndDate,
                         1  as TimesheetStatusId,
                         N'Я' as TimesheetCode,
                         isnull(wc.[IsWorkingHours],0) as TimesheetHours,
                         u.Id as UserId,
                         u.Name as UserName,
                         1 as IsDismissalDay
                         from [dbo].[Users] u
                         inner join [dbo].[WorkingCalendar] wc on wc.Date = u.DateRelease");
            //sqlQuery += @" inner join [dbo].[Users] u on u.Id = v.UserId ";
            sqlQuery += string.Format(@" where 
                          u.DateRelease between :beginDate and :endDate");
            sqlQuery += " and u.Id in (:userList)";
            //switch (userRole)
            //{
            //    case UserRole.Employee:
            //        sqlQuery += " and v.UserId = :userId ";
            //        break;
            //    case UserRole.Manager:
            //        sqlQuery += " and u.ManagerId = :userId ";
            //        break;
            //    case UserRole.PersonnelManager:
            //        sqlQuery += " and exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ";//" and u.PersonnelManagerId = :userId ";
            //        break;
            //    default:
            //        throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));

            //}
            sqlQuery += string.Format(" order by UserName,UserId,EndDate");
            IQuery query = Session.CreateSQLQuery(sqlQuery).
               AddScalar("BeginDate", NHibernateUtil.DateTime).
               AddScalar("EndDate", NHibernateUtil.DateTime).
               AddScalar("TimesheetStatusId", NHibernateUtil.Int32).
               AddScalar("TimesheetCode", NHibernateUtil.String).
               AddScalar("TimesheetHours", NHibernateUtil.Int32).
               AddScalar("UserId", NHibernateUtil.Int32).
               AddScalar("UserName", NHibernateUtil.String).
               AddScalar("IsDismissalDay", NHibernateUtil.Boolean).
               SetDateTime("beginDate", beginDate).
               SetDateTime("endDate", endDate).
               SetParameterList("userList", userIds);
            //SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(RequestDto))).List<RequestDto>();
        }

        protected IList<RequestDto> GetRequestsForDismissal(DateTime beginDate, DateTime endDate,
        int managerId, UserRole managerRole, IList<int> userIds)
        {
            string sqlQuery =
                string.Format(@"select v.EndDate as BeginDate,
                         v.EndDate as EndDate,
                         1  as TimesheetStatusId,
                         N'Я' as TimesheetCode,
                         isnull(wc.[IsWorkingHours],0) as TimesheetHours,
                         u.Id as UserId,
                         u.Name as UserName
                         from dbo.Dismissal v
                         inner join [dbo].[WorkingCalendar] wc on wc.Date = v.EndDate");
            sqlQuery += @" inner join [dbo].[Users] u on u.Id = v.UserId ";
            sqlQuery += string.Format(@" where 
                          v.EndDate between :beginDate and :endDate
                          and v.DeleteDate is null 
                          and v.UserDateAccept is not  null
                          and v.ManagerDateAccept is not null
                          and v.PersonnelManagerDateAccept is not null ");
            sqlQuery += " and u.Id in (:userList)";
            //switch (userRole)
            //{
            //    case UserRole.Employee:
            //        sqlQuery += " and v.UserId = :userId ";
            //        break;
            //    case UserRole.Manager:
            //        sqlQuery += " and u.ManagerId = :userId ";
            //        break;
            //    case UserRole.PersonnelManager:
            //        sqlQuery += " and exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ";//" and u.PersonnelManagerId = :userId ";
            //        break;
            //    default:
            //        throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));

            //}
            sqlQuery += string.Format(" order by UserName,UserId,EndDate");
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
               SetParameterList("userList", userIds);
               //SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(RequestDto))).List<RequestDto>();
        }

        protected IList<RequestDto> GetRequestsForEmploymentAndDaysBefore(DateTime beginDate, DateTime endDate,
                int managerId, UserRole managerRole, IList<int> userIds)
        {
            // menu Employment is hide
            //IList<RequestDto> list = GetRequestsForEmployment(beginDate, endDate, managerId, managerRole,userIds);
            //foreach (RequestDto requestDto in list.Where(requestDto => requestDto.TimesheetCode == PresenceStatusCode))
            //    requestDto.TimesheetHours = 8;
            IList<RequestDto> list = GetAcceptedUser(beginDate, endDate, managerId, managerRole, userIds);
            //foreach (RequestDto requestDto in list)
            //{
            //    DayOfWeek dayOfweek = requestDto.BeginDate.DayOfWeek;
            //    if ((dayOfweek == DayOfWeek.Sunday) || (dayOfweek == DayOfWeek.Saturday))
            //    {
            //        requestDto.TimesheetCode = HolidayStatusCode;
            //        requestDto.TimesheetHours = new int?();
            //        requestDto.TimesheetStatusId = 0;
            //    }
            //}
            IList<RequestDto> beforeList = list.Where(x => x.BeginDate > beginDate).Select(requestDto => new RequestDto
                                                                         {
                                                                             BeginDate = beginDate, 
                                                                             EndDate = requestDto.BeginDate.AddDays(-1), 
                                                                             TimesheetCode = EmptyStatusCode, 
                                                                             TimesheetHours = new int?(), 
                                                                             TimesheetStatusId = 0, 
                                                                             UserId = requestDto.UserId, 
                                                                             UserName = requestDto.UserName,
                                                                         }).ToList();
            List<RequestDto> result = list.ToList();
            result.AddRange(beforeList);
            return result;
        }
        protected IList<RequestDto> GetAcceptedUser(DateTime beginDate, DateTime endDate,
                int managerId, UserRole managerRole, IList<int> userIds)
        {
            string sqlQuery =
                string.Format(@"select u.DateAccept as BeginDate,
                         u.DateAccept as EndDate,
                         1  as TimesheetStatusId,
                         N'Я' as TimesheetCode,
                         isnull(wc.[IsWorkingHours],0) as TimesheetHours,
                         u.Id as UserId,
                         u.Name as UserName,
                         1 as IsEmploymentDay
                         from [dbo].[Users] u
                         inner join [dbo].[WorkingCalendar] wc on wc.Date = u.DateAccept");
            sqlQuery += string.Format(@" where 
                          u.DateAccept between :beginDate and :endDate");
            sqlQuery += " and u.Id in (:userList)";
            sqlQuery += string.Format(" order by UserName,UserId,BeginDate");
            IQuery query = Session.CreateSQLQuery(sqlQuery).
               AddScalar("BeginDate", NHibernateUtil.DateTime).
               AddScalar("EndDate", NHibernateUtil.DateTime).
               AddScalar("TimesheetStatusId", NHibernateUtil.Int32).
               AddScalar("TimesheetCode", NHibernateUtil.String).
               AddScalar("TimesheetHours", NHibernateUtil.Int32).
               AddScalar("UserId", NHibernateUtil.Int32).
               AddScalar("UserName", NHibernateUtil.String).
               AddScalar("IsEmploymentDay", NHibernateUtil.Boolean).
               SetDateTime("beginDate", beginDate).
               SetDateTime("endDate", endDate).
               SetParameterList("userList", userIds);
            //SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(RequestDto))).List<RequestDto>();
        }
        protected IList<RequestDto> GetRequestsForEmployment(DateTime beginDate, DateTime endDate,
                int managerId, UserRole managerRole, IList<int> userIds)
        {
            string sqlQuery =
                string.Format(@"select v.BeginDate as BeginDate,
                         v.BeginDate as EndDate,
                         v.TimesheetStatusId  as TimesheetStatusId,
                         ts.[ShortName] as TimesheetCode,
                         isnull(wc.[IsWorkingHours],0) as TimesheetHours,
                         u.Id as UserId,
                         u.Name as UserName,
                         1 as IsEmploymentDay,
                         from dbo.Employment v
                         inner join [dbo].[WorkingCalendar] wc on wc.Date = v.BeginDate");
            sqlQuery += @" inner join [dbo].[TimesheetStatus] ts on ts.Id = v.TimesheetStatusId
                           inner join [dbo].[Users] u on u.Id = v.UserId ";
            sqlQuery += string.Format(@" where 
                          v.BeginDate between :beginDate and :endDate
                          and v.DeleteDate is null 
                          and v.UserDateAccept is not  null
                          and v.ManagerDateAccept is not null
                          and v.PersonnelManagerDateAccept is not null ");
            sqlQuery += " and u.Id in (:userList)";
            //switch (userRole)
            //{
            //    case UserRole.Employee:
            //        sqlQuery += " and v.UserId = :userId ";
            //        break;
            //    case UserRole.Manager:
            //        sqlQuery += " and u.ManagerId = :userId ";
            //        break;
            //    case UserRole.PersonnelManager:
            //        sqlQuery += " and exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ";//" and u.PersonnelManagerId = :userId ";
            //        break;
            //    default:
            //        throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));

            //}
            sqlQuery += string.Format(" order by UserName,UserId,BeginDate");
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
               SetParameterList("userList", userIds);
               //SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(RequestDto))).List<RequestDto>();
        }


        protected IList<RequestDto> GetRequestsForType(DateTime beginDate,DateTime endDate,RequestTypeEnum type,
            int managerId, UserRole managerRole, IList<int> userIds)
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
                case RequestTypeEnum.ChildVacation:
                    sqlQuery += @" from [dbo].[ChildVacation] v ";
                    break;
                case RequestTypeEnum.Absence:
                    sqlQuery += @" from [dbo].[Absence] v ";
                    break;
                case RequestTypeEnum.Sicklist:
                    sqlQuery += @" from [dbo].[Sicklist] v ";
                    break;
                case RequestTypeEnum.Mission:
                    sqlQuery += @" from [dbo].[Mission] v ";
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
            sqlQuery += " and u.Id in (:userList)";
            //switch(userRole)
            //{
            //    case UserRole.Employee:
            //        sqlQuery += " and v.UserId = :userId ";
            //        break;
            //    case UserRole.Manager:
            //        sqlQuery += " and u.ManagerId = :userId ";
            //        break;
            //    case UserRole.PersonnelManager:
            //        sqlQuery += " and exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ";//" and u.PersonnelManagerId = :userId ";
            //        break;
            //    default:
            //        throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}",userRole));

            //}
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
               SetParameterList("userList", userIds);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(RequestDto))).List<RequestDto>();
        }
        protected IList<RequestDto> GetRequests(DateTime beginDate,DateTime endDate,
            int managerId, UserRole managerRole, IList<IdNameDtoWithDates> users)
        {

            List<int> userIds = users.ToList().ConvertAll(x => x.Id).ToList();
            List<RequestDto> allList = new List<RequestDto>();
            allList.AddRange(GetRequestsForType(beginDate, endDate, RequestTypeEnum.Vacation, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForType(beginDate, endDate, RequestTypeEnum.ChildVacation, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForType(beginDate, endDate, RequestTypeEnum.Absence, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForType(beginDate, endDate, RequestTypeEnum.Sicklist, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForType(beginDate, endDate, RequestTypeEnum.Mission, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForTypeOneDay(beginDate, endDate, RequestTypeEnum.HolidayWork, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForTypeOneDay(beginDate, endDate, RequestTypeEnum.TimesheetCorrection, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForEmploymentAndDaysBefore(beginDate, endDate, managerId, managerRole, userIds));
            allList.AddRange(GetRequestsForDismissalAndDaysAfter(beginDate, endDate, managerId, managerRole, userIds));

            return allList;
        }
        public IList<DayRequestsDto> GetRequestsForMonth(
            int month, int year, int managerId, UserRole managerRole,
            IList<DayRequestsDto> dtoList, IList<IdNameDtoWithDates> users,
            IList<WorkingCalendar> workDays, IList<TerraGraphicDbDto> tgList
            )
        {
            IList<RequestDto> requests = GetRequests(dtoList.First().Day, dtoList.Last().Day,
                managerId, managerRole, users);
            foreach (var idNameDto in users)
            {
                if ((idNameDto.DateAccept.HasValue &&
                    ((idNameDto.DateAccept.Value.Year < year) ||
                    ((idNameDto.DateAccept.Value.Year == year)
                      && (idNameDto.DateAccept.Value.Month <= month)))) &&
                    (!idNameDto.DateRelease.HasValue ||
                    ((idNameDto.DateRelease.Value.Year > year) ||
                    ((idNameDto.DateRelease.Value.Year == year)
                      && (idNameDto.DateRelease.Value.Month >= month))))
                    )
                {
                    List<decimal> userStats = new List<decimal> { 0,0,0,0,0 };
                    List<int> userStatsDays = new List<int> { 0, 0, 0, 0, 0 }; 
                    foreach (var dayRequestsDto in dtoList)
                    {
                        DateTime date = dayRequestsDto.Day;
                        //DayOfWeek dayOfweek = date.DayOfWeek;
                        int? workHours = CoreUtils.GetHoursForDay(workDays, date);
                        bool isHoliday = CoreUtils.IsDayHoliday(workDays, date);//(dayOfweek == DayOfWeek.Sunday) || (dayOfweek == DayOfWeek.Saturday);
                        List<RequestDto> userRequestList = requests.Where(x =>
                                                                          (date >= x.BeginDate && date <= x.EndDate &&
                                                                           idNameDto.Id == x.UserId)).ToList();
                        if (userRequestList.Count == 0)
                        {
                            TerraGraphicDbDto tg =
                                tgList.Where(x => x.UserId == idNameDto.Id && x.Day == date).FirstOrDefault();
                            bool isDayInFuture = DateTime.Today < date;
                            decimal? timesheetHours;
                            if (tg != null)
                                timesheetHours = tg.Hours;
                            else
                                timesheetHours = isHoliday ? new decimal?() : isDayInFuture ? new int?() : workHours;
                            RequestDto newDto = new RequestDto
                                                    {
                                                        BeginDate = date,
                                                        EndDate = date,
                                                        TimesheetCode = isHoliday
                                                                            ? HolidayStatusCode
                                                                            : isDayInFuture
                                                                                  ? EmptyStatusCode
                                                                                  : PresenceStatusCode,
                                                        TimesheetHours = timesheetHours,
                                                        UserId = idNameDto.Id,
                                                        UserName = idNameDto.Name
                                                    };
                            dayRequestsDto.Requests.Add(newDto);
                            if(tg != null)
                            {
                                userStats[0] += tg.Hours;
                                userStatsDays[0]++;
                            }
                            else
                             AddToUserStats(userStats, userStatsDays, new List<RequestDto> {newDto}, isHoliday,workHours);
                        }
                        else
                        {
                            AddToUserStats(userStats,userStatsDays,userRequestList,isHoliday,workHours);
                        }
                        dayRequestsDto.Requests.AddRange(userRequestList);
                    }
                    idNameDto.userStats = userStats;
                    idNameDto.userStatsDays = userStatsDays;
                }

            }
            return dtoList;
        }
        public IList<DayRequestsDto> GetRequestsForYear(DateTime beginDate, DateTime year, int managerId, UserRole managerRole,
            IList<DayRequestsDto> dtoList, IList<IdNameDtoWithDates> users, IList<WorkingCalendar> workDays,
            IList<TerraGraphicDbDto> tgList)
        {
            IList<RequestDto> requests = GetRequests(dtoList.First().Day, dtoList.Last().Day,
                managerId, managerRole, users);
            foreach (var idNameDto in users)
            {
                //if ((idNameDto.DateAccept.HasValue &&
                //    ((idNameDto.DateAccept.Value.Year < year) ||
                //    ((idNameDto.DateAccept.Value.Year == year)
                //      && (idNameDto.DateAccept.Value.Month <= month)))) &&
                //    (!idNameDto.DateRelease.HasValue ||
                //    ((idNameDto.DateRelease.Value.Year > year) ||
                //    ((idNameDto.DateRelease.Value.Year == year)
                //      && (idNameDto.DateRelease.Value.Month >= month))))
                //    )
                //{
                    List<decimal> userStats = new List<decimal> { 0, 0, 0, 0, 0 };
                    List<int> userStatsDays = new List<int> { 0, 0, 0, 0, 0 };
                    foreach (var dayRequestsDto in dtoList)
                    {
                        DateTime date = dayRequestsDto.Day;
                        int? workHours = CoreUtils.GetHoursForDay(workDays, date);
                        bool isHoliday = CoreUtils.IsDayHoliday(workDays, date);//(dayOfweek == DayOfWeek.Sunday) || (dayOfweek == DayOfWeek.Saturday);
                        List<RequestDto> userRequestList = requests.Where(x =>
                                                                          (date >= x.BeginDate && date <= x.EndDate &&
                                                                           idNameDto.Id == x.UserId)).ToList();
                        if (userRequestList.Count == 0)
                        {
                            //bool isDayInFuture = DateTime.Today < date;
                            TerraGraphicDbDto tg = tgList.Where(x => x.UserId == idNameDto.Id && x.Day == date).FirstOrDefault();
                            bool isDayInFuture = DateTime.Today < date;
                            decimal? timesheetHours;
                            if (tg != null)
                                timesheetHours = tg.Hours;
                            else
                                timesheetHours = isHoliday ? new int?() : isDayInFuture ? new int?() : workHours;

                            RequestDto newDto = new RequestDto
                            {
                                BeginDate = date,
                                EndDate = date,
                                TimesheetCode = isHoliday ? HolidayStatusCode
                                        : isDayInFuture ? EmptyStatusCode : PresenceStatusCode,
                                TimesheetHours = timesheetHours,//isHoliday ? new int?() : isDayInFuture ? new int?() : workHours.Value,
                                UserId = idNameDto.Id,
                                UserName = idNameDto.Name
                            };
                            dayRequestsDto.Requests.Add(newDto);
                            if (tg != null)
                            {
                                userStats[0] += tg.Hours;
                                userStatsDays[0]++;
                            }
                            else
                                AddToUserStats(userStats, userStatsDays, new List<RequestDto> { newDto }, isHoliday, workHours);
                        }
                        else
                        {
                            AddToUserStats(userStats, userStatsDays, userRequestList, isHoliday, workHours);
                        }
                        dayRequestsDto.Requests.AddRange(userRequestList);
                    }
                    idNameDto.userStats = userStats;
                    idNameDto.userStatsDays = userStatsDays;
                }

            //}
            return dtoList;
        }
   
        protected void AddToUserStats(List<decimal> userStats, List<int> userStatsDays, 
            List<RequestDto> userRequestList,bool isHoliday,int? workHours)
        {
            if (isHoliday)
                return;
            foreach (RequestDto dto in userRequestList)
            {
                switch(dto.TimesheetCode)
                {
                    case "Я":
                        //userStats[0] += dto.TimesheetHours.HasValue?dto.TimesheetHours.Value:0;     
                        //break;
                    case "ВЧ":
                    case "Н":
                    case "РВ":
                    case "С":
                        userStats[0] += workHours.HasValue?workHours.Value:0;//dto.TimesheetHours.HasValue?dto.TimesheetHours.Value:0;
                        userStatsDays[0]++;
                    break;
                    case "Б":
                    case "Т":
                        userStats[2] += workHours.HasValue ? workHours.Value : 0;
                        userStatsDays[2]++;
                        break;
                    case "К":
                        userStats[3] += workHours.HasValue ? workHours.Value : 0;
                        userStatsDays[3]++;
                        break;
                    case "ОТ":
                    case "ОЗ":
                    case "ДО":
                    case "Р":
                    case "ОЖ":
                        userStats[1] += workHours.HasValue ? workHours.Value : 0;
                        userStatsDays[1]++;
                        break;
                    case "В":
                    case EmptyStatusCode:
                        break;
                    default:
                        userStats[4] += workHours.HasValue?workHours.Value:0;//dto.TimesheetHours.HasValue ? dto.TimesheetHours.Value : workHours.Value;
                        userStatsDays[4]++;
                        break;
                }
            }
        }
    }
}