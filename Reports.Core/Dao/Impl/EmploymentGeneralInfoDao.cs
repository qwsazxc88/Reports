using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentGeneralInfoDao : DefaultDao<GeneralInfo>, IEmploymentGeneralInfoDao
    {
        public EmploymentGeneralInfoDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public int? GetDocumentId(int userId)
        {
            string sqlQuery = string.Format(@"select gi.Id from GeneralInfo gi where gi.Candidate =
                                                (select ec.Id from EmploymentCandidate ec where ec.User = :userId)");
            IQuery query = Session.CreateQuery(sqlQuery);
            query.SetInt32("userId", userId);
            return query.UniqueResult<int>();
        }
    }
}