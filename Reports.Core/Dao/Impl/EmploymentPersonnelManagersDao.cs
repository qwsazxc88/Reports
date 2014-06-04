using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentPersonnelManagersDao : DefaultDao<PersonnelManagers>, IEmploymentPersonnelManagersDao
    {
        public EmploymentPersonnelManagersDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}