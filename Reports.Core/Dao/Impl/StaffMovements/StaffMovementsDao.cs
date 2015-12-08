using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Services;
using Reports.Core.Utils;
using NHibernate.Linq;
using NHibernate.Transform;
using NHibernate;
namespace Reports.Core.Dao.Impl
{
    public class StaffMovementsDao: DefaultDao<StaffMovements>,IStaffMovementsDao
    {
        public StaffMovementsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
            
        }
        public decimal GetUserSalary(int UserId)
        {
            var query = Session.CreateSQLQuery(String.Format("select Salary from vwStaffPostSalary where userid={0}", UserId));
            //query.AddScalar("Salary", NHibernateUtil.Decimal);
            return query.UniqueResult<Decimal>();            
        }
        public decimal GetUserRegionCoeff(int UserId)
        {
            var query = Session.CreateSQLQuery(String.Format("select Regional from vwStaffPostSalary where userid={0}", UserId));
            return query.UniqueResult<Decimal>();  
            /*query.AddScalar("Regional", NHibernateUtil.Decimal);
            var result = query.SetResultTransformer(Transformers.AliasToBean(typeof(decimal))).List<decimal>();
            if (result != null && result.Any())
                return result.First();
            return 0;*/
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
