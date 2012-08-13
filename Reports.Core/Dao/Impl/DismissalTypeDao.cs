using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DismissalTypeDao : DefaultDao<DismissalType>, IDismissalTypeDao
    {
        public DismissalTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}