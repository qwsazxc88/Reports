using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistPaymentRestrictTypeDao : DefaultDao<SicklistPaymentRestrictType>,
                                                  ISicklistPaymentRestrictTypeDao
    {
        public SicklistPaymentRestrictTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}