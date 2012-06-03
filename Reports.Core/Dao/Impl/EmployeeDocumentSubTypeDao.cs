using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmployeeDocumentSubTypeDao : DefaultDao<EmployeeDocumentSubType>, IEmployeeDocumentSubTypeDao
    {
        public EmployeeDocumentSubTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual bool IsSubTypeWithNameAndOtherIdExists(string name, int id,int typeId)
        {
            return (int)Session.CreateCriteria(typeof(EmployeeDocumentSubType))
                              .Add(Restrictions.Eq("Name", name))
                              .Add(Restrictions.Eq("Type.Id", typeId))
                              .Add(Restrictions.Not(Restrictions.Eq("Id", id)))
                              .SetProjection(Projections.RowCount())
                              .UniqueResult() > 0;
        }

    }
}