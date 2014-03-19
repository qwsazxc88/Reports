using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentDao : DefaultDao<Appointment>, IAppointmentDao
    {
        public AppointmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}