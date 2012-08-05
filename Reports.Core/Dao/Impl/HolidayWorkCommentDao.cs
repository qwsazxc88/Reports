using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HolidayWorkCommentDao : DefaultDao<HolidayWorkComment>, IHolidayWorkCommentDao
    {
        public HolidayWorkCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}