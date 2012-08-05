using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HolidayWorkTypeDao : DefaultDao<HolidayWorkType>, IHolidayWorkTypeDao
    {
        public HolidayWorkTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}