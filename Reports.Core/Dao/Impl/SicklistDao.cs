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

        public IList<SicklistDto> GetSicklistDocuments(
               int userId, 
               UserRole role,
               int departmentId,
               int positionId,
               int typeId,
               int statusId,
               int paymentPercentTypeId,
               DateTime? beginDate,
               DateTime? endDate,
               string userName, 
               int sortedBy,
               bool? sortDescending
            )
        {
            string sqlQuery =
                string.Format(sqlSelectForListSicklist,
                    DeleteRequestText,
                    "dbo.SicklistType",
                    "v.[CreateDate]",
                    "Болезнь (неявка)",
                    "[dbo].[Sicklist]",
                    "v.[BeginDate]",
                    "v.[EndDate]"
                );

            string whereString = GetWhereForUserRole(role, userId);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);

            return query.SetResultTransformer(Transformers.AliasToBean<SicklistDto>()).List<SicklistDto>();

            //return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();

            //return GetDefaultDocuments(userId, role, departmentId,
            //    positionId, typeId,
            //    statusId, beginDate, endDate,userName,
            //    sqlQuery, sortedBy, sortDescending);

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
                AddScalar("UserExperienceIn1C", NHibernateUtil.Boolean);                
        }

        public bool ResetApprovals(int id)
        {
            var sicklist = Session.Get<Sicklist>(id);
            if (sicklist != null)
            {
                var transaction = Session.BeginTransaction();
                sicklist.ManagerDateAccept = null;
                sicklist.UserDateAccept = null;
                Session.Update(sicklist);
                transaction.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}