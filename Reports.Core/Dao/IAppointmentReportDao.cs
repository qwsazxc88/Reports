using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IAppointmentReportDao : IDao<AppointmentReport>
    {
        List<AppointmentReport> LoadForAppointmentId(int id);
        bool IsApprovedReportForAppointmentIdExists(int id);
    }
}