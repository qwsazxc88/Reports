using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpVersionDao : DefaultDao<HelpVersion>, IHelpVersionDao
    {
        public HelpVersionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}