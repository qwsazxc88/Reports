using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TimesheetDayDao : DefaultDao<TimesheetDay>, ITimesheetDayDao
    {
        public TimesheetDayDao(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }
    }
}