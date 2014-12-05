using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpServiceTypeDao : DefaultDaoSorted<HelpServiceType>, IHelpServiceTypeDao
    {
        public HelpServiceTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}