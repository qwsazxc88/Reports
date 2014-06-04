using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentGeneralInfoDao : DefaultDao<GeneralInfo>, IEmploymentGeneralInfoDao
    {
        public EmploymentGeneralInfoDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}