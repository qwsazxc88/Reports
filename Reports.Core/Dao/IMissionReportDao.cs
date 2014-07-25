using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionReportDao : IDao<MissionReport>
    {
        IList<MissionReportDto> GetDocuments(int userId,
                                                    UserRole role,
                                                    int departmentId,
                                                    int statusId,
                                                    DateTime? beginDate,
                                                    DateTime? endDate,
                                                    string userName,
                                                    int sortBy,
                                                    bool? sortDescending);

        bool IsReportForOrderExists(int orderId);
        List<MissionReport> GetReportsWithPurchaseBookReportCosts(int userId);
        MissionReport GetReportForOrder(int orderId);
    }
}