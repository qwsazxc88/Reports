using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DeductionDao : DefaultDao<Deduction>, IDeductionDao
    {
        public DeductionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}