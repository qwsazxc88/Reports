using System.Collections.Generic;
using Reports.Core.Domain;
using System;
using Reports.Core.Dto;
namespace Reports.Core.Dao
{
    public interface IAppointmentReportDao : IDao<AppointmentReport>
    {
        List<AppointmentReport> LoadForAppointmentId(int id);
        bool IsApprovedReportForAppointmentIdExists(int id);
        /*List<AppointmentDto> GetDocuments(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                int sortBy,
                bool? sortDescending);*/
    }
}