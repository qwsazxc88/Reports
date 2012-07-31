using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistTypeDao : DefaultDao<SicklistType>, ISicklistTypeDao
    {
        public SicklistTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}