using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentCommonDao : DefaultDao<EmploymentCandidate>, IEmploymentCommonDao
    {
        public EmploymentCommonDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public int GetDocumentId<T>(int userId)
        {
            string sqlQuery = string.Format(@"select gi.Id from {0} gi where gi.Candidate =
                                                (select ec.Id from EmploymentCandidate ec where ec.User = :userId)", typeof(T).Name);
            IQuery query = Session.CreateQuery(sqlQuery);
            query.SetInt32("userId", userId);
            return query.UniqueResult<int>();
        }

        public T GetEntityById<T>(int id)
        {
            var entity = Session.Get<T>(id);

            return entity;
        }

        public EmploymentCandidate GetCandidateByUserId(int userId)
        {
            string sqlQuery = string.Format(@"select ec.id from EmploymentCandidate ec where ec.User = :userId");
            IQuery query = Session.CreateQuery(sqlQuery);
            query.SetInt32("userId", userId);
            int? id = query.UniqueResult<int>();
            return Session.Get<EmploymentCandidate>(id);
        }

        // Запись обновленных полей документа в БД
        public bool SaveOrUpdateDocument<T>(T entity)
        {
                var transaction = Session.BeginTransaction();
                Session.SaveOrUpdate(entity);
                transaction.Commit();
                return true;
        }

    }
}