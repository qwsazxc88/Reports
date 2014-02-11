using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionPurchaseBookDocumentDao : DefaultDao<MissionPurchaseBookDocument>,
                                                  IMissionPurchaseBookDocumentDao
    {
        public MissionPurchaseBookDocumentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<MissionPurchaseBookDocDto> GetDocuments(
              UserRole role,
              DateTime? beginDate,
              DateTime? endDate,
              int sortBy,
              bool? sortDescending)
        {
            string sqlQuery = @"select v.Id as Id,Number as DocumentNumber,
                                DocumentDate,CfNumber,CfDate,
                                c.Name as Contractor,[Sum]
                                from dbo.MissionPurchaseBookDocument v
                                inner join dbo.Contractor c on c.Id = v.ContractorId";
            //string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            //whereString = GetTypeWhere(whereString, typeId);
            //whereString = GetStatusWhere(whereString, statusId);
            string whereString = GetDatesWhere(string.Empty,beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            //whereString = GetDepartmentWhere(whereString, departmentId);
            //whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);
            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionPurchaseBookDocDto))).List<MissionPurchaseBookDocDto>();
        }
        public override string GetDatesWhere(string whereString, DateTime? beginDate,
            DateTime? endDate)
        {
            if (beginDate.HasValue && endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" (((v.[DocumentDate] >= :beginDate) and (v.[DocumentDate] < :endDate)) 
                                or ((v.[CfDate] >= :beginDate) and (v.[CfDate]  < :endDate))) ";
            }
            else if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" ((v.[DocumentDate] >= :beginDate) or (v.[CfDate] >= :beginDate)) ";
            }
            else if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"((v.[DocumentDate] < :endDate) or (v.[CfDate] < :endDate)) ";
            }
            return whereString;
        }
        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
                  int sortedBy,
                  bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
            {
                orderBy = " ORDER BY Id DESC";
                return string.Format(MissionOrderDao.sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY Id DESC";
                    return string.Format(MissionOrderDao.sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by DocumentNumber";
                    break;
                case 2:
                    orderBy += @" order by DocumentDate";
                    break;
                case 3:
                    orderBy = @" order by Contractor";
                    break;
                case 4:
                    orderBy = @" order by [Sum]";
                    break;
                case 5:
                    orderBy = @" order by CfNumber";
                    break;
                case 6:
                    orderBy += @" order by CfDate";
                    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(MissionOrderDao.sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("DocumentNumber", NHibernateUtil.String).
                AddScalar("DocumentDate", NHibernateUtil.DateTime).
                AddScalar("CfNumber", NHibernateUtil.String).
                AddScalar("CfDate", NHibernateUtil.DateTime).
                AddScalar("Contractor", NHibernateUtil.String).
                AddScalar("Sum", NHibernateUtil.Decimal).
                AddScalar("Number", NHibernateUtil.Int32);

        }
        public virtual void AddDatesToQuery(IQuery query, DateTime? beginDate,
           DateTime? endDate)
        {
            if (beginDate.HasValue)
                query.SetDateTime("beginDate", beginDate.Value);
            if (endDate.HasValue)
                query.SetDateTime("endDate", endDate.Value.AddDays(1));
        }
    }
}