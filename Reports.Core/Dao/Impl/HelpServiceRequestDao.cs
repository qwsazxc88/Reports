using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpServiceRequestDao : DefaultDao<HelpServiceRequest>, IHelpServiceRequestDao
    {
        public HelpServiceRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}