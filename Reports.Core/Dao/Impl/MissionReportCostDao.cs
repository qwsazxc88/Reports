using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionReportCostDao : DefaultDao<MissionReportCost>, IMissionReportCostDao
    {
        public MissionReportCostDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual IList<IdNameDto> GetCostTypesWithPurchaseBookReportCosts(int reportId)
        {
            string sqlQuery = string.Format(@"select mrct.Id,mrct.Name 
                                                from dbo.MissionReportCostType mrct
                                                inner join [dbo].[MissionReportCost] mrc on mrc.TypeId = mrct.Id
                                                where mrc.IsCostFromPurchaseBook = 1 and mrc.ReportId = {0}
                                                order by Name", reportId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }
    }
}