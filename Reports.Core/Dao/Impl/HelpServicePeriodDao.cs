using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpServicePeriodDao : DefaultDaoSorted<HelpServicePeriod>, IHelpServicePeriodDao
    {
        public HelpServicePeriodDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}