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
    public class DismissalDao : DefaultDao<Dismissal>, IDismissalDao
    {
        public DismissalDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<VacationDto> GetDocuments(
            int userId, 
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int statusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName, 
            int sortedBy,
            bool? sortDescending,
            string Number
            )
        {
            string sqlQuery =
                string.Format(sqlSelectForListDismissal, 
                                DeleteRequestText,
                                "dbo.DismissalType",
                                "v.[CreateDate]",
                                "Увольнение",
                                "[dbo].[Dismissal]",
                                "v.[EndDate]",
                                "v.[EndDate]");

            return GetDismissalDocuments(userId, role, departmentId,
                positionId, typeId,
                statusId, beginDate, endDate,userName, 
                sqlQuery,sortedBy,sortDescending, Number);

            #region Deleted
            //inner join [dbo].[UserToDepartment] ud on u.Id = ud.UserId";
            //string whereString = GetWhereForUserRole(role, userId);
            //whereString = GetTypeWhere(whereString, typeId);
            //whereString = GetStatusWhere(whereString, statusId);
            //whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            //whereString = GetDepartmentWhere(whereString, departmentId);
            //sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString);
            /*if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1:
                        statusWhere =
                            @"UserDateAccept is null and ManagerDateAccept is null and PersonnelManagerDateAccept is null and SendTo1C is null";
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
                        statusWhere =
                            @"UserDateAccept is not null and ManagerDateAccept is not null and PersonnelManagerDateAccept is not null";
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
            /*if (typeId != 0)
                query.SetInt32("typeId", typeId);
            if (beginDate.HasValue)
                query.SetDateTime("beginDate", beginDate.Value);
            if (endDate.HasValue)
                query.SetDateTime("endDate", endDate.Value.AddDays(1)); 
            if (positionId != 0)
                query.SetInt32("positionId", positionId);
            if (departmentId != 0)
                query.SetInt32("departmentId", departmentId);*/
            //return query.SetResultTransformer(Transformers.AliasToBean(typeof (VacationDto))).List<VacationDto>();
            #endregion
        }
        public virtual IList<VacationDto> GetDismissalDocuments(
                                int userId,
                                UserRole role,
                                int departmentId,
                                int positionId,
                                int typeId,
                                int statusId,
                                DateTime? beginDate,
                                DateTime? endDate,
                                string userName,
                                string sqlQuery,
                                int sortedBy,
                                bool? sortDescending,
                                string Number
            )
        {
            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDissmissalDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetDocumentNumberWhere(whereString, Number);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            query.SetInt32("userId", userId);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }
        public virtual string GetDissmissalDatesWhere(string whereString, DateTime? beginDate,
            DateTime? endDate)
        {
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EndDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EndDate] < :endDate ";
            }
            return whereString;
        }
        public DateTime? GetDismissalDateForUser(int userId)
        {
            string sqlQuery =
                (@"select min(EndDate) from Dismissal
                    where DeleteDate is null and SendTo1C is not null
                    and UserId = :userId");
            ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
            query.SetInt32("userId", userId);
            return query.List<DateTime?>().FirstOrDefault();
        }

        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime).
                AddScalar("BeginDate", NHibernateUtil.DateTime).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestStatus", NHibernateUtil.String).
                AddScalar("IsOriginalReceived", NHibernateUtil.Boolean).
                AddScalar("IsPersonnelFileSentToArchive", NHibernateUtil.Boolean).
                AddScalar("Dep7Name",NHibernateUtil.String).
                AddScalar("Dep3Name",NHibernateUtil.String).
                AddScalar("Position",NHibernateUtil.String);
        }

        public IList<Dismissal> LoadForIdsList(List<int> ids)
        {
            if (ids.Count == 0)
                return new List<Dismissal>();
            ICriteria criteria = Session.CreateCriteria(typeof(Dismissal));
            criteria.Add(Restrictions.In("Id", ids));
            return criteria.List<Dismissal>();
        }
    }
    
}