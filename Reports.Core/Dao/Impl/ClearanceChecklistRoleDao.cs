using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ClearanceChecklistRoleDao : DefaultDao<ClearanceChecklistRole>, IClearanceChecklistRoleDao
    {
        public ClearanceChecklistRoleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public IList<ClearanceChecklistRole> GetClearanceChecklistRoles()
        {
            return Session.CreateCriteria<ClearanceChecklistRole>().List<ClearanceChecklistRole>();
        }

        public IList<User> GetClearanceChecklistRoleAuthorities(ClearanceChecklistRole clearanceChecklistRole)
        {
            var roleOwners = new List<User>();
            foreach(var roleOwner in clearanceChecklistRole.RoleOwners)
            {
                roleOwners.Add(roleOwner);
            }
            return roleOwners;
        }
    }
}
