using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistDao : DefaultDao<Sicklist>, ISicklistDao
    {
        public SicklistDao(ISessionManager sessionManager)
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
               int paymentPercentTypeId,
               DateTime? beginDate,
               DateTime? endDate,
               int sortedBy,
               bool? sortDescending
            )
        {
            string sqlQuery = string.Format(sqlSelectForList,
                    DeleteRequestText,
                    "dbo.SicklistType",
                    "v.[CreateDate]",
                    "Болезнь (неявка)",
                    "[dbo].[Sicklist]",
                    "v.[BeginDate]",
                    "v.[EndDate]");

            return GetDefaultDocuments(userId, role, departmentId,
                positionId, typeId,
                statusId, beginDate, endDate, sqlQuery, sortedBy, sortDescending);

//            string sqlQuery =
//                string.Format(@"select v.Id as Id,
//                         u.Id as UserId,
//                         'Болезнь (неявка) '+ u.Name + case when [DeleteDate] is not null then N' ({0})' else '' end as Name,
//                         v.[CreateDate] as Date    
//            from [dbo].[Sicklist] v
//            inner join [dbo].[Users] u on u.Id = v.UserId",DeleteRequestText);
//            string whereString = GetWhereForUserRole(role, userId);
//            whereString = GetTypeWhere(whereString, typeId);
//            whereString = GetStatusWhere(whereString, statusId);
//            whereString = GetDatesWhere(whereString, beginDate, endDate);
//            whereString = GetPositionWhere(whereString, positionId);
//            whereString = GetDepartmentWhere(whereString, departmentId);
            
//            if (paymentPercentTypeId != 0)
//            {
//                if (whereString.Length > 0)
//                    whereString += @" and ";
//                whereString += @"v.[PaymentPercentId] = :paymentPercentTypeId ";
//            }
//            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString,0, null);
//            IQuery query = CreateQuery(sqlQuery);
//            AddDatesToQuery(query, beginDate, endDate);
//            if (paymentPercentTypeId != 0)
//                query.SetInt32("paymentPercentTypeId", paymentPercentTypeId);
//            return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }
    }
}