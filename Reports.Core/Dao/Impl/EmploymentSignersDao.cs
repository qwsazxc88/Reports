using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentSignersDao : DefaultDao<Signer>, IEmploymentSignersDao
    {
        public EmploymentSignersDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}