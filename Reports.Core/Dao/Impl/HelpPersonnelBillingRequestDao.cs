using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpPersonnelBillingRequestDao : DefaultDao<HelpPersonnelBillingRequest>,
                                                  IHelpPersonnelBillingRequestDao
    {
        public HelpPersonnelBillingRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}