using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using Reports.Core;
namespace Reports.Presenters.UI.Bl
{
    public interface ISurchargeBL
    {
        bool AddSurcharge(int userId, float sum, int creatorId, DateTime editDate, int missionReportId);
        IList<SurchargeDto> GetDocuments(int userId, UserRole role, int departmentId, int statusId, DateTime? beginDate, DateTime? endDate, string userName, int sortedBy, bool? sortDescending, string Number);
    }
}
