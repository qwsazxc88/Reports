using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpServiceProductionTimeDao : DefaultDaoSorted<HelpServiceProductionTime>, IHelpServiceProductionTimeDao
    {
        public HelpServiceProductionTimeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}