using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TimesheetCorrectionCommentDao : DefaultDao<TimesheetCorrectionComment>, ITimesheetCorrectionCommentDao
    {
        public TimesheetCorrectionCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}