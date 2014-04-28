using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentCommentDao : DefaultDao<AppointmentComment>, IAppointmentCommentDao
    {
        public AppointmentCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}