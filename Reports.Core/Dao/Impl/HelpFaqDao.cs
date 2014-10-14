using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpFaqDao : DefaultDao<HelpFaq>, IHelpFaqDao
    {
        public HelpFaqDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual List<HelpFaq> LoadAllSortedByQuestion()
        {
            return Session.Query<HelpFaq>().OrderBy(a => a.Question).ToList();
        }
    }
}