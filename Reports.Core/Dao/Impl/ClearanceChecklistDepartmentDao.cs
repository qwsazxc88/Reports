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
            // TODO: Replace with implementation
            // return new List<ClearanceChecklistDepartment>();
        }

        public IList<User> GetClearanceChecklistDepartmentAuthorities(int clearanceChecklistDepartmentId)
        {
            ExtendedRole extendedRole = Session.CreateCriteria(typeof(ClearanceChecklistDepartment))
                .Add(Restrictions.Eq("Id", clearanceChecklistDepartmentId)).UniqueResult<ClearanceChecklistDepartment>().ExtendedRole;
            var roleOwners = new List<User>();
            foreach(var roleOwner in extendedRole.RoleOwners)
            {
                roleOwners.Add(roleOwner);
            }
            return roleOwners;
            //IList<User> users = Session.CreateCriteria(typeof(User))
            //    /*.Add(Restrictions.Where<User>(u => u.ExtendedRoles.Contains(extendedRole)))*/.List<User>();
            //return Session.CreateCriteria(typeof(User))
            //    .Add(Restrictions.Where<User>(u => u.ExtendedRoles.Contains(extendedRole))).List<User>();
            // TODO: Replace with implementation
            //return new List<User>();
        }
    }
}
