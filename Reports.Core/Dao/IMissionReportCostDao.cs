using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionReportCostDao : IDao<MissionReportCost>
    {
        IList<IdNameDto> GetCostTypesWithPurchaseBookReportCosts(int reportId);
    }
}