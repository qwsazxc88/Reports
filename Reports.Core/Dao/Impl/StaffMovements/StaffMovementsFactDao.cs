using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NHibernate.Linq;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Services;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Dao;
namespace Reports.Core.Dao.Impl
{
    public class StaffMovementsFactDao: DefaultDao<StaffMovementsFact>, IStaffMovementsFactDao
    {
        public StaffMovementsFactDao(ISessionManager sessionManager)
            : base(sessionManager)
        {       
           
        }
        public IList<StaffMovementsFact> GetDocuments(int UserId, UserRole role, int Number, int SEPReqId, int SMId, int DepartmentId, string userName)
        {
            var DepDao = Ioc.Resolve<IDepartmentDao>();
            var ManualRoleRecordDao = Ioc.Resolve<IManualRoleRecordDao>();
            var query = Session.QueryOver<StaffMovementsFact>();
            
            User userToMove = null;
            Department userToMoveDepartment = null;
            query = query.JoinAlias(x => x.User, () => userToMove);
            query = query.JoinAlias(x => userToMove.Department, () => userToMoveDepartment);
            var user = UserDao.Load(UserId);
            if(userName!=null)
                userName=userName.Trim();
            var restriction = Restrictions.Conjunction();
            if (Number > 0)
            {
                restriction.Add(Restrictions.Where<StaffMovementsFact>(x => x.Id == Number));
            }
            if (SEPReqId > 0)
            {
                restriction.Add(Restrictions.Where<StaffMovementsFact>(x => x.StaffEstablishedPostRequest.Id == SEPReqId));
            }
            if (SMId > 0)
            {
                restriction.Add(Restrictions.Where<StaffMovementsFact>(x=>x.StaffMovements.Id == SMId));
            }
            if (DepartmentId > 0)
            {
                var department =DepDao.Load(DepartmentId);
                var userdepprop = Projections.Property(() => userToMoveDepartment.Path);
                restriction.Add(Restrictions.Like(userdepprop,department.Path+"%"));
            }
            if (!String.IsNullOrEmpty(userName))
            {
                var usernameprop = Projections.Property(() => userToMove.Name);
                restriction.Add(Restrictions.InsensitiveLike(usernameprop,userName+"%"));
            }
            switch (role)
            {
                case UserRole.Manager :
                    var depts = ManualRoleRecordDao.QueryExpression(x => x.User.Id == UserId && x.TargetDepartment != null).Select(x=>x.TargetDepartment).ToList();
                    if (depts == null) depts = new List<Department>();
                    depts.Add(user.Department);
                    List<int> deptsIds = new List<int>();
                    foreach (var d in depts)
                    {
                        deptsIds.Add(d.Id);
                        var cd = DepDao.GetChildDepartments(d);
                        if (cd != null && cd.Any()) deptsIds.AddRange(cd.Select(x => x.Id).ToList());
                    }
                    restriction.Add(Restrictions.In(Projections.Property(()=>userToMoveDepartment.Id),deptsIds));
                    break;
                case UserRole.Employee:
                    restriction.Add(Restrictions.Where<StaffMovementsFact>(x => x.User.Id == UserId));
                    break;
                case UserRole.PersonnelManager:
                    var users = UserDao.GetUsersForPersonnel(UserId);
                    if (users == null) users = new List<User>();
                    restriction.Add(Restrictions.In(Projections.Property(() => userToMove.Id), users.Select(x => x.Id).ToList()));
                    break;
                case UserRole.Admin:
                case UserRole.ConsultantPersonnel:
                case UserRole.OutsourcingManager:
                    break;
                default: throw new Exception("Нет доступа");
            }
            return query.Where(restriction).List();

        }
    }

}
