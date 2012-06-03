using System.Linq;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SettingsDao : DefaultDao<Settings>, ISettingsDao
    {
        public SettingsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual Settings LoadFirst()
        {
            return Session.CreateCriteria(typeof (Settings))
                .List<Settings>().FirstOrDefault();
        }
    }
}