using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentEducationTypeDao : DefaultDao<AppointmentEducationType>, IAppointmentEducationTypeDao
    {
        public AppointmentEducationTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}