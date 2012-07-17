using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class VacationDao : DefaultDao<Vacation>, IVacationDao
    {
        public VacationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<VacationDto> GetDocuments(
                UserRole role,
                int departmentId,
                int positionId,
                int vacationTypeId,
                int requestStatusId,
                DateTime? beginDate,
                DateTime? endDate)
        {
            string sqlQuery =
                @"select v.Id as Id,
                         u.Id as UserId,
                         cast(v.Id as nvarchar(10)) as Name,
                         v.[CreateDate] as Date    
            from [dbo].[Vacation] v
            inner join [dbo].[Users] u on u.Id = v.UserId
            inner join [dbo].[UserToDepartment] ud on u.Id = ud.UserId";
            string whereString = string.Empty;
            if (requestStatusId != 0)
                whereString += @"v.[StatusId] = :statusId ";
            if (vacationTypeId != 0)
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
                whereString += @"v.[CreateDate] <= :endDate ";
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
                whereString += @"ud.[DepartmentId] = :departmentId ";
            }

            if (whereString.Length > 0)
                sqlQuery += @" where " + whereString;
            sqlQuery += @" order by Date DESC,Name ";

            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime);
            if (requestStatusId != 0)
                query.SetInt32("statusId", requestStatusId);
            if (vacationTypeId != 0)
                query.SetInt32("typeId", vacationTypeId);
            if (beginDate.HasValue)
                query.SetDateTime("beginDate", beginDate.Value);
            if (endDate.HasValue)
                query.SetDateTime("endDate", endDate.Value.AddDays(1).AddMilliseconds(-1));
            if (positionId != 0)
                query.SetInt32("positionId", positionId);
            if (positionId != 0)
                query.SetInt32("departmentId", departmentId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }
    }
}