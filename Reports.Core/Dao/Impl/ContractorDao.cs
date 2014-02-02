using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ContractorDao : DefaultDao<Contractor>, IContractorDao
    {
        public ContractorDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}