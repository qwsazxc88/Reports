using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник адресов.
    /// </summary>
    public class RefAddressesDao : DefaultDao<RefAddresses>, IRefAddressesDao
    {
        public RefAddressesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
