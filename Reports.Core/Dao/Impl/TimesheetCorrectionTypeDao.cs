using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TimesheetCorrectionTypeDao : DefaultDao<TimesheetCorrectionType>, ITimesheetCorrectionTypeDao
    {
        public TimesheetCorrectionTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}