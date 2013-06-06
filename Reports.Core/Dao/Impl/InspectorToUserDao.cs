using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class InspectorToUserDao : DefaultDao<InspectorToUser>, IInspectorToUserDao
    {
        public InspectorToUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual bool IsInspectorToUserRecordExists(int inspectorId, int userId )
        {
            return (int)Session.CreateCriteria(typeof(InspectorToUser))
                              .Add(Restrictions.Eq("Inspector.Id", inspectorId))
                              .Add(Restrictions.Eq("User.Id", userId))
                              .SetProjection(Projections.RowCount())
                              .UniqueResult() > 0;
        }
    }
}