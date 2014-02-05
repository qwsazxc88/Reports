using System.Collections.Generic;
using NHibernate;
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
    }
}