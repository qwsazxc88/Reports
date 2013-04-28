using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionDao : DefaultDao<Mission>, IMissionDao
    {
        public MissionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<VacationDto> GetDocuments(
               int userId, 
               UserRole role,
               int departmentId,
               int positionId,
               int typeId,
               int requestStatusId,
               DateTime? beginDate,
               DateTime? endDate,
               int sortedBy,
               bool? sortDescending)
        {
            string sqlQuery = string.Format(sqlSelectForList,
                                            DeleteRequestText,
                                            "dbo.MissionType",
                                            "v.[CreateDate]",
                                            "Командировка",
                                            "[dbo].[Mission]");

            return GetDefaultDocuments(userId, role, departmentId,
                positionId, typeId,
                requestStatusId, beginDate, endDate, sqlQuery, sortedBy, sortDescending);

//            string sqlQuery =
//                string.Format(@"select v.Id as Id,
//                         u.Id as UserId,
//                         'Командировка '+ u.Name + case when [DeleteDate] is not null then N' ({0})' else '' end as Name,
//                         v.[CreateDate] as Date    
//            from [dbo].[Mission] v
//            inner join [dbo].[Users] u on u.Id = v.UserId",DeleteRequestText);
//            return GetDefaultDocuments(userId, role, departmentId,
//                positionId, typeId,
//                requestStatusId, beginDate, endDate, sqlQuery, 0, null);
            //inner join [dbo].[UserToDepartment] ud on u.Id = ud.UserId";
            //string whereString = GetWhereForUserRole(role, userId);
            //whereString = GetTypeWhere(whereString, typeId);
            //whereString = GetStatusWhere(whereString, requestStatusId);
            //whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            //whereString = GetDepartmentWhere(whereString, departmentId);
            //sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString);
            /*if (requestStatusId != 0)
            {
                string statusWhere;
                switch (requestStatusId)
                {
                    case 1:
                        statusWhere = @"UserDateAccept is null and ManagerDateAccept is null and PersonnelManagerDateAccept is null and SendTo1C is null";
                        break;
                    case 2:
                        statusWhere = @"UserDateAccept is not null";
                        break;
                    case 3:
                        statusWhere = @"UserDateAccept is null";
                        break;
                    case 4:
                        statusWhere = @"ManagerDateAccept is not null";
                        break;
                    case 5:
                        statusWhere = @"ManagerDateAccept is null";
                        break;
                    case 6:
                        statusWhere = @"PersonnelManagerDateAccept is not null";
                        break;
                    case 7:
                        statusWhere = @"PersonnelManagerDateAccept is null";
                        break;
                    case 8:
                        statusWhere = @"UserDateAccept is not null and ManagerDateAccept is not null and PersonnelManagerDateAccept is not null";
                        break;
                    case 9:
                        statusWhere = @"SendTo1C is not null";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere + " ";
            }
            if (typeId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[TypeId] = :typeId ";
            }
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[CreateDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[CreateDate] < :endDate ";
            }
            if (positionId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"u.[PositionId] = :positionId ";
            }
            if (departmentId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"u.[DepartmentId] = :departmentId ";
            }

            if (whereString.Length > 0)
                sqlQuery += @" where " + whereString;
            sqlQuery += @" order by Date DESC,Name ";
            */
            //IQuery query = CreateQuery(sqlQuery);
                /*Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime);*/

            //AddDatesToQuery(query, beginDate, endDate);
            //if (requestStatusId != 0)
            //    query.SetInt32("statusId", requestStatusId);
            /*if (typeId != 0)
                query.SetInt32("typeId", typeId);
            if (beginDate.HasValue)
                query.SetDateTime("beginDate", beginDate.Value);
            if (endDate.HasValue)
                query.SetDateTime("endDate", endDate.Value.AddDays(1));//.AddDays(1).AddMilliseconds(-1));
            if (positionId != 0)
                query.SetInt32("positionId", positionId);
            if (departmentId != 0)
                query.SetInt32("departmentId", departmentId);*/
            //return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }
    }
}