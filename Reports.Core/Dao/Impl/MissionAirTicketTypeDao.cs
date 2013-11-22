using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionAirTicketTypeDao : DefaultDao<MissionAirTicketType>, IMissionAirTicketTypeDao
    {
        public MissionAirTicketTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}