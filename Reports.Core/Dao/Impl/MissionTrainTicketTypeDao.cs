using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionTrainTicketTypeDao : DefaultDao<MissionTrainTicketType>, IMissionTrainTicketTypeDao
    {
        public MissionTrainTicketTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}