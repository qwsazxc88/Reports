using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistBabyMindingTypeDao : DefaultDao<SicklistBabyMindingType>, ISicklistBabyMindingTypeDao
    {
        public SicklistBabyMindingTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}