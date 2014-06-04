using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentContactsDao : DefaultDao<Contacts>, IEmploymentContactsDao
    {
        public EmploymentContactsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}