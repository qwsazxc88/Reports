using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TerraPointToUserDao : DefaultDao<TerraPointToUser>, ITerraPointToUserDao
    {
        public TerraPointToUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual TerraPointToUser FindByUserId(int userId)
        {
            return (TerraPointToUser)Session.CreateCriteria(typeof(TerraPointToUser))
                .Add(Restrictions.Eq("UserId", userId)).UniqueResult();
        }
    }
}