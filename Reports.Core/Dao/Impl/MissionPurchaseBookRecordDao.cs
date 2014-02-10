using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionPurchaseBookRecordDao : DefaultDao<MissionPurchaseBookRecord>,
                                                IMissionPurchaseBookRecordDao
    {
        public MissionPurchaseBookRecordDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<MissionPurchaseBookRecordDto> GetRecordsForDocumentId(int documentId)
        {
            const string sqlQuery = @"select pbr.Id,u.Name+N' ,'+d.Name+' ,'+d3.Name as UserName,
                                    mr.Number as ReportNumber,
                                    mo.Number as OrderNumber,
                                    pbr.Sum,SumNds,pbr.AllSum,mrct.Name as CostType,
                                    RequestNumber,
                                    case when mr.AccountantDateAccept is null then 1 else 0 end as IsEditable
                                    from dbo.MissionPurchaseBookRecord pbr
                                    inner join dbo.Users u on u.Id = pbr.UserId
                                    inner join dbo.Department d on d.Id = u.DepartmentId
                                    inner join dbo.Department d3 on d.Path like d3.Path + N'%' and d3.ItemLevel = 3
                                    inner join dbo.MissionOrder mo on mo.Id = pbr.MissionOrderId
                                    inner join dbo.MissionReportCost mrc on mrc.Id = pbr.MissionReportCostId
                                    inner join dbo.MissionReport mr on mr.Id = mrc.ReportId
                                    inner join dbo.MissionReportCostType mrct on mrct.Id = pbr.MissionReportCostTypeId
                    where pbr.MissionPurchaseBookDocumentId = :documentId  
                    order by UserName";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("ReportNumber", NHibernateUtil.Int32).
                AddScalar("OrderNumber", NHibernateUtil.Int32).
                AddScalar("Sum", NHibernateUtil.Decimal).
                AddScalar("SumNds", NHibernateUtil.Decimal).
                AddScalar("AllSum", NHibernateUtil.Decimal).
                AddScalar("CostType", NHibernateUtil.String).
                AddScalar("RequestNumber", NHibernateUtil.String).
                AddScalar("IsEditable", NHibernateUtil.Boolean).
                SetInt32("documentId", documentId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionPurchaseBookRecordDto)))
                .List<MissionPurchaseBookRecordDto>();
        }
        public virtual IList<MissionPurchaseBookRecord> GetRecordsForCost(int costId)
        {
            return Session.CreateCriteria(typeof(MissionPurchaseBookRecord))
                  .Add(Restrictions.Eq("MissionReportCost.Id", costId))
                  .List<MissionPurchaseBookRecord>();
        }
        public IList<MissionPbRecordListDto> GetDocuments(
               UserRole role,
               int departmentId,
               //int statusId,
               //DateTime? beginDate,
               //DateTime? endDate,
               string userName,
               int sortBy,
               bool? sortDescending)
        {
            string sqlQuery = @"select v.Id,u.Name as UserName,u.Cnilc,mo.Number as MissionOrderNumber,
                            c.Name as ContractorName,c.Account as ContractorAccount,
                            pbd.DocumentDate as DocDate,pbd.Number as DocNumber,ct.Name as CostTypeName,
                            v.[AllSum] 
                            from dbo.MissionPurchaseBookRecord v
                            inner join [dbo].[Users] u on u.Id = v.UserId
                            inner join dbo.MissionReportCostType ct on ct.Id = v.MissionReportCostTypeId
                            inner join dbo.MissionOrder mo on mo.Id = v.MissionOrderId
                            inner join dbo.MissionPurchaseBookDocument pbd on pbd.Id = v.MissionPurchaseBookDocumentId
                            inner join dbo.Contractor c on c.Id = pbd.ContractorId";
            //string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            //whereString = GetTypeWhere(whereString, typeId);
            //whereString = GetStatusWhere(whereString, statusId);
            //whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            string whereString = GetDepartmentWhere(string.Empty, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            if (!string.IsNullOrEmpty(userName))
                query.SetString("userName", "%" + userName.ToLower() + "%");
            //AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionPbRecordListDto))).List<MissionPbRecordListDto>();
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
                    orderBy = @" order by UserName";
                    break;
                case 2:
                    orderBy += @" order by Cnilc";
                    break;
                case 3:
                    orderBy = @" order by MissionOrderNumber";
                    break;
                case 4:
                    orderBy = @" order by ContractorName";
                    break;
                case 5:
                    orderBy += @" order by ContractorAccount";
                    break;
                case 6:
                    orderBy = @" order by DocDate";
                    break;
                case 7:
                    orderBy = @" order by DocNumber";
                    break;
                case 8:
                    orderBy = @" order by AllSum";
                    break;
                case 9:
                    orderBy = @" order by CostTypeName";
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
        //     public int Number { get; set; }
        //public string UserName { get; set; }
        //public string Cnilc { get; set; }
        //public string MissionOrderNumber { get; set; }
        //public string ContractorName { get; set; }
        //public string ContractorAccount { get; set; }
        //public DateTime DocDate { get; set; }
        //public string DocNumber { get; set; }
        //public string CostTypeName { get; set; }
        //public decimal AllSum { get; set; }
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("Cnilc", NHibernateUtil.String).
                AddScalar("MissionOrderNumber", NHibernateUtil.String).
                AddScalar("ContractorName", NHibernateUtil.String).
                AddScalar("ContractorAccount", NHibernateUtil.String).
                AddScalar("DocDate", NHibernateUtil.DateTime).
                AddScalar("DocNumber", NHibernateUtil.String).
                AddScalar("CostTypeName", NHibernateUtil.String).
                AddScalar("AllSum", NHibernateUtil.Decimal);
        }
    }
}