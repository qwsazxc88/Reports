using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Services;
using Reports.Core.Utils;
using NHibernate.Linq;
namespace Reports.Core.Dao.Impl
{
    public class StaffMovementsDao: DefaultDao<StaffMovements>,IStaffMovementsDao
    {
        public StaffMovementsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
            
        }
        public IList<StaffMovements> GetDocuments(int UserId, UserRole role, int DepartmentId, string UserName, int Number, int Status)
        {
            var CurrentUser = UserDao.Load(UserId);
            var mandepts=Session.Query<ManualRoleRecord>()
                .Where(x => x.Role.Id == 1 && x.TargetDepartment != null && x.User.Id == UserId )
                .Select(x => x.TargetDepartment).Distinct()
                .ToList();
            
            var query=Session.Query<StaffMovements>();
            switch (role)
            {
                case UserRole.Employee:
                    query=query.Where(x => x.User.Id == CurrentUser.Id);
                    break;
                case UserRole.Manager:
                    var predicate = PredicateBuilder.False<StaffMovements>();
                    predicate = predicate.Or(x=>x.SourceManager.Id == CurrentUser.Id);
                    predicate = predicate.Or(x => x.TargetManager.Id == CurrentUser.Id);
                    if (CurrentUser.Department != null)
                    {
                        predicate = predicate.Or(x => x.SourceDepartment.Path.StartsWith(CurrentUser.Department.Path));
                        predicate = predicate.Or(x => x.TargetDepartment.Path.StartsWith(CurrentUser.Department.Path));
                    }
                    foreach (var dep in mandepts)
                    {
                        predicate = predicate.Or(x => x.SourceDepartment.Path.StartsWith(dep.Path));
                        predicate = predicate.Or(x => x.TargetDepartment.Path.StartsWith(dep.Path));
                    }
                    query = query.Where(predicate);
                    break;
                case UserRole.PersonnelManager:
                    query=query.Where(x => x.User.Personnels.Any(y => y.Id == CurrentUser.Id) );
                    break;
                case UserRole.ConsultantPersonnel:
                    break;
            }
            if (DepartmentId > 0)
                query=query.Where(x => x.SourceDepartment.Id == DepartmentId);
            if(!String.IsNullOrWhiteSpace(UserName))
                query=query.Where(x => x.User.Name.Contains(UserName));
            if(Status>0)
                query=query.Where(x => x.Status.Id == Status);
            if(Number>0)
                query=query.Where(x => x.Id == Number);
            return query.ToList();
        }
    }
}
