using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RoleDao : DefaultDao<Role>, IRoleDao
    {
        public RoleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<Role> LoadAllSorted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(Role));
            criteria.AddOrder(new Order("Name", true));
            return criteria.List<Role>();
        }
    }
}