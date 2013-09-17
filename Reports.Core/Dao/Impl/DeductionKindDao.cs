using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DeductionKindDao : DefaultDao<DeductionKind>, IDeductionKindDao
    {
        public DeductionKindDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}