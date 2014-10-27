using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpServiceTransferMethodDao : DefaultDaoSorted<HelpServiceTransferMethod>, IHelpServiceTransferMethodDao
    {
        public HelpServiceTransferMethodDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}