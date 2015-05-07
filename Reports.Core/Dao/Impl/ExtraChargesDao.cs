using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ExtraChargesDao : DefaultDao<ExtraCharges>, IExtraChargesDao
    {
        public ExtraChargesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
