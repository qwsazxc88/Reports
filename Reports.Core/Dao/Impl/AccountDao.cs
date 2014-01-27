using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AccountDao : DefaultDao<Account>, IAccountDao
    {
        public AccountDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}