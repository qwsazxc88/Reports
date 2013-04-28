﻿using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class VacationDao : DefaultDao<Vacation>, IVacationDao
    {
        public VacationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public int GetRequestCountsForUserAndDates(DateTime beginDate,
            DateTime endDate,int userId,int vacationId)
        {
            int requestCount = GetRequestsCountForType(beginDate,
                                    endDate, RequestTypeEnum.Vacation, userId,
                                    UserRole.Employee, vacationId);
            if (requestCount > 0)
            {
                Log.DebugFormat("Fount {0} Vacation requests for {1} {2} {3} {4}"
                    ,requestCount
                    ,beginDate,endDate,userId,vacationId);
                return requestCount;
            }
            requestCount = GetRequestsCountForType(beginDate,
                        endDate, RequestTypeEnum.Absence, userId,
                        UserRole.Employee, vacationId);
            if (requestCount > 0)
            {
                Log.DebugFormat("Fount {0} Absence requests for {1} {2} {3} {4}"
                , requestCount
                , beginDate, endDate, userId, vacationId);
                return requestCount;
            }
            requestCount = GetRequestsCountForType(beginDate,
                        endDate, RequestTypeEnum.Sicklist, userId,
                        UserRole.Employee, vacationId);
            if (requestCount > 0)
            {
                Log.DebugFormat("Fount {0} Sicklist requests for {1} {2} {3} {4}"
                , requestCount
                , beginDate, endDate, userId, vacationId);
                return requestCount;
            }
            requestCount = GetRequestsCountForType(beginDate,
                        endDate, RequestTypeEnum.Mission, userId,
                        UserRole.Employee, vacationId);
            if (requestCount > 0)
            {
                Log.DebugFormat("Fount {0} Mission requests for {1} {2} {3} {4}"
                    , requestCount
                    , beginDate, endDate, userId, vacationId);
                return requestCount;
            }

            requestCount = GetRequestsCountForTypeOneDay(beginDate,
                endDate, RequestTypeEnum.HolidayWork, userId,
                UserRole.Employee);
            if (requestCount > 0)
            {
                Log.DebugFormat("Fount {0} HolidayWork requests for {1} {2} {3} {4}"
                    , requestCount
                    , beginDate, endDate, userId, vacationId);
                return requestCount;
            }

            requestCount = GetRequestsCountForTypeOneDay(beginDate,
                        endDate, RequestTypeEnum.TimesheetCorrection, userId,
                        UserRole.Employee);
            if (requestCount > 0)
            {
                Log.DebugFormat("Fount {0} TimesheetCorrection requests for {1} {2} {3} {4}"
                    , requestCount
                    , beginDate, endDate, userId, vacationId);
                return requestCount;
            }

            requestCount = GetRequestsCountForDismissal(
                            endDate, userId,
                            UserRole.Employee);
            if(requestCount > 0)
            {
                Log.DebugFormat("Fount {0} Dismissal requests for {1} {2} {3}"
                   , requestCount
                   , endDate, userId, vacationId);
            }
            else
            {
                Log.DebugFormat("No requests found for {0} {1} {2} {3}",
                   beginDate, endDate, userId, vacationId);
            }
            return requestCount;

        }
        public IList<VacationDto> GetDocuments(
                int userId,
                UserRole role,
                int departmentId,
                int positionId,
                int vacationTypeId,
                int requestStatusId,
                DateTime? beginDate,
                DateTime? endDate,
                int sortedBy,
                bool? sortDescending)
        {
//            string sqlQuery =
//                string.Format(@"select v.Id as Id,
//                         u.Id as UserId,
//                         N'Отпуск '+ u.Name + case when [DeleteDate] is not null then N' ({0})' else '' end as Name,
//                         v.[CreateDate] as Date    
//            from [dbo].[Vacation] v
//            inner join [dbo].[Users] u on u.Id = v.UserId",DeleteRequestText);

            string sqlQuery = string.Format(sqlSelectForList,
                                DeleteRequestText,
                                "dbo.VacationType",
                                "v.[CreateDate]",
                                "Отпуск",
                                "[dbo].[Vacation]");

            return GetDefaultDocuments(userId, role, departmentId,
                positionId, vacationTypeId,
                requestStatusId, beginDate, endDate, sqlQuery,sortedBy,sortDescending);
            //inner join [dbo].[UserToDepartment] ud on u.Id = ud.UserId";
            //string whereString = GetWhereForUserRole(role,userId);

            //if (requestStatusId != 0)
            //{
            //    string statusWhere;
            //    switch (requestStatusId)
            //    {
            //        case 1:
            //            statusWhere = @"UserDateAccept is null and ManagerDateAccept is null and PersonnelManagerDateAccept is null and SendTo1C is null";
            //        break;
            //        case 2:
            //            statusWhere = @"UserDateAccept is not null";
            //        break;
            //        case 3:
            //            statusWhere = @"UserDateAccept is null";
            //        break;
            //        case 4:
            //            statusWhere = @"ManagerDateAccept is not null";
            //        break;
            //        case 5:
            //            statusWhere = @"ManagerDateAccept is null";
            //        break;
            //        case 6:
            //            statusWhere = @"PersonnelManagerDateAccept is not null";
            //        break;
            //        case 7:
            //            statusWhere = @"PersonnelManagerDateAccept is null";
            //        break;
            //        case 8:
            //            statusWhere = @"UserDateAccept is not null and ManagerDateAccept is not null and PersonnelManagerDateAccept is not null";
            //        break;
            //        case 9:
            //            statusWhere = @"SendTo1C is not null";
            //        break;
            //        default:
            //            throw new ArgumentException("Неправильный статус заявки");
            //    }
            //    if (whereString.Length > 0)
            //        whereString += @" and ";
            //    whereString += @" "+statusWhere+" ";
            //}
            //    //whereString += @"v.[StatusId] = :statusId ";
            //if (vacationTypeId != 0)
            //{
            //    if (whereString.Length > 0)
            //        whereString += @" and ";
            //    whereString += @"v.[TypeId] = :typeId ";
            //}
            //if (beginDate.HasValue)
            //{
            //    if (whereString.Length > 0)
            //        whereString += @" and ";
            //    whereString += @"v.[CreateDate] >= :beginDate ";
            //}
            //if (endDate.HasValue)
            //{
            //    if (whereString.Length > 0)
            //        whereString += @" and ";
            //    whereString += @"v.[CreateDate] < :endDate ";
            //}
            //if (positionId != 0)
            //{
            //    if (whereString.Length > 0)
            //        whereString += @" and ";
            //    whereString += @"u.[PositionId] = :positionId ";
            //}
            //if (departmentId != 0)
            //{
            //    if (whereString.Length > 0)
            //        whereString += @" and ";
            //    whereString += @"u.[DepartmentId] = :departmentId ";
            //}

            //if (whereString.Length > 0)
            //    sqlQuery += @" where " + whereString;
            //sqlQuery += @" order by Date DESC,Name ";

            //IQuery query = Session.CreateSQLQuery(sqlQuery).
            //    AddScalar("Id", NHibernateUtil.Int32).
            //    AddScalar("UserId", NHibernateUtil.Int32).
            //    AddScalar("Name", NHibernateUtil.String).
            //    AddScalar("Date", NHibernateUtil.DateTime);
            ////if (requestStatusId != 0)
            ////    query.SetInt32("statusId", requestStatusId);
            //if (vacationTypeId != 0)
            //    query.SetInt32("typeId", vacationTypeId);
            //if (beginDate.HasValue)
            //    query.SetDateTime("beginDate", beginDate.Value);
            //if (endDate.HasValue)
            //    query.SetDateTime("endDate", endDate.Value.AddDays(1));//.AddDays(1).AddMilliseconds(-1));
            //if (positionId != 0)
            //    query.SetInt32("positionId", positionId);
            //if (departmentId != 0)
            //    query.SetInt32("departmentId", departmentId);
            //return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }
    }
}