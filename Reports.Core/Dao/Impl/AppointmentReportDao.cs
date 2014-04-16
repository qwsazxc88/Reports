using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentReportDao : DefaultDao<AppointmentReport>, IAppointmentReportDao
    {
        public AppointmentReportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}