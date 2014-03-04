using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ClearanceChecklistDepartmentDao : DefaultDao<ClearanceChecklistDepartment>, IClearanceChecklistDepartmentDao
    {
        public ClearanceChecklistDepartmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public IList<ClearanceChecklistDepartment> GetClearanceChecklistDepartments()
        {
            return Session.CreateCriteria(typeof(ClearanceChecklistDepartment))
                   .AddOrder(new Order("Id", true))
                   .List<ClearanceChecklistDepartment>();
        }

        public IList<User> GetClearanceChecklistDepartmentAuthorities(int clearanceChecklistDepartmentId)
        {
            ClearanceChecklistRole clearanceChecklistRole = Session.CreateCriteria(typeof(ClearanceChecklistDepartment))
                .Add(Restrictions.Eq("Id", clearanceChecklistDepartmentId)).UniqueResult<ClearanceChecklistDepartment>().ClearanceChecklistRole;
            var roleOwners = new List<User>();
            foreach(var roleOwner in clearanceChecklistRole.RoleOwners)
            {
                roleOwners.Add(roleOwner);
            }
            return roleOwners;
        }
    }
}
