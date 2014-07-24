using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class CountryDao : DefaultDao<Country>, ICountryDao
    {
        public CountryDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}