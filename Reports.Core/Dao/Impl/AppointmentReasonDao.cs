using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentReasonDao : DefaultDao<AppointmentReason>, IAppointmentReasonDao
    {
        public AppointmentReasonDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}