using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpVersionDao : DefaultDao<HelpVersion>, IHelpVersionDao
    {
        public HelpVersionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual List<HelpVersion> LoadAllSortedByDate()
        {
            return Session.Query<HelpVersion>().OrderByDescending(a => a.ReleaseDate).ToList();
        }
    }
}