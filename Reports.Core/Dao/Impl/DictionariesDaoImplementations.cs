using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpBillingTitleDao : DefaultDaoSorted<HelpBillingTitle>, IHelpBillingTitleDao
    {
        public HelpBillingTitleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
    public class HelpBillingUrgencyDao : DefaultDaoSorted<HelpBillingUrgency>, IHelpBillingUrgencyDao
    {
        public HelpBillingUrgencyDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}