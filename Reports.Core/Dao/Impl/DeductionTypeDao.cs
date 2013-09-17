using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DeductionTypeDao : DefaultDao<DeductionType>, IDeductionTypeDao
    {
        public DeductionTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}