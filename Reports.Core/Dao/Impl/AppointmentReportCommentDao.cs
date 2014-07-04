using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentReportCommentDao:DefaultDao<AppointmentReportComment>, IAppointmentReportCommentDao
    {
         public AppointmentReportCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
