using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ChiefToUserDao : DefaultDao<ChiefToUser>, IChiefToUserDao
    {
        public ChiefToUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IChiefToUserDao Members

        public virtual bool IsChiefToUserRecordExists(int inspectorId, int userId)
        {
            return (int) Session.CreateCriteria(typeof (ChiefToUser))
                             .Add(Restrictions.Eq("Inspector.Id", inspectorId))
                             .Add(Restrictions.Eq("User.Id", userId))
                             .SetProjection(Projections.RowCount())
                             .UniqueResult() > 0;
        }

        #endregion
    }
}