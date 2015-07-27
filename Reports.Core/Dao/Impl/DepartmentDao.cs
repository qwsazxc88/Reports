using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;
using System.Linq;
using NHibernate.Linq;

namespace Reports.Core.Dao.Impl
{
    public class DepartmentDao : DefaultDao<Department>, IDepartmentDao
    {
        //public const string NameFieldName = "Name";

        public DepartmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<Department> SearchByName(string name)
        {
            return Session.CreateCriteria(typeof(Department))
                   .Add(Restrictions.Like("Name", name+"%"))
                   .AddOrder(new Order("Name", true))
                   .List<Department>();
        }
        public IList<Department> SearchByParentId(int parentId)
        {
            return Session.CreateCriteria(typeof(Department))
                   .Add(Restrictions.Eq("ParentId", parentId))
                   .AddOrder(new Order("Name", true))
                   .List<Department>();
        }
        public Department SearchByNameDistinct(string name)
        {
            return (Department)Session.CreateCriteria(typeof(Department))
                   .Add(Restrictions.Eq("Name", name)).UniqueResult();
        }
        public Department GetRootDepartment()
        {
            return (Department)Session.CreateCriteria(typeof(Department))
                   .Add(Restrictions.IsNull("ParentId")).UniqueResult();
        }
        public virtual IList<Reports.Core.Dto.DepartmentDto> GetDepartmentsForManager23(int managerId, int level, bool dep3only)
        {
            string sqlQuery = string.Format(@" select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentManager23ToDepartment3] mtod
                                        inner join [dbo].[Department] d on d.Id = mtod.[DepartmentId]
                                        where mtod.managerId = {0}", managerId);
            if (level == 2 && !dep3only)
                sqlQuery += string.Format(@" union
                            select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentCreateManager2ToDepartment2] mctod
                                        inner join [dbo].[Department] d on d.Id = mctod.[DepartmentId]
                                        where mctod.managerId = {0}", managerId);

            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(Reports.Core.Dto.DepartmentDto))).List<Reports.Core.Dto.DepartmentDto>();
        }
        public IList<Department> GetDepartmentsTree(int departmentId)
        {
            const string sqlQuery = (@";with Parents as
                (select d1.Id,d1.Name
                ,d1.parentId
                ,d1.path, d1.itemlevel from dbo.Department d
                inner join dbo.Department d1 on d.Path like d1.Path+'%' 
                where d.Id = :departmentId)
                select d1.Id, d1.Version, d1.Code, d1.Name, d1.Code1C
                , d1.ParentId, d1.Path, d1.ItemLevel
                from dbo.Department d1
                inner join Parents p on d1.path like p.path+'%'
                and d1.ItemLevel = p.ItemLevel+1
                order by d1.itemlevel 
                ");
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Version", NHibernateUtil.Int32).
                AddScalar("Code", NHibernateUtil.String).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Code1C", NHibernateUtil.Int32).
                AddScalar("ParentId", NHibernateUtil.Int32).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32).
                SetInt32("departmentId", departmentId).
                SetResultTransformer(Transformers.AliasToBean(typeof(Department))).
                List<Department>();
        }
        public Department GetParentDepartmentWithLevel(Department dep,int level)
        {
            return Session.Query<Department>().
                Where(x => x.ItemLevel == level && dep.Path.StartsWith(x.Path))
                .FirstOrDefault();
        }
        public IList<User> GetDepartmentManagers(int departmentId, bool allLevels = false)
        {
            IList<User> managers;
            if (allLevels)
            {
                Department department = Get(departmentId);
                if (department == null)
                {
                    return new List<User>();
                }
                managers = Session.Query<User>()
                    .Where<User>(user => (user.RoleId & (int)UserRole.Manager) > 0 && department.Path.StartsWith(user.Department.Path) && user.IsActive == true)
                    .ToList<User>();
            }
            else
            {
                managers = Session.Query<User>()
                    .Where<User>(user => (user.RoleId & (int)UserRole.Manager) > 0 && user.Department.Id == departmentId && user.IsActive == true)
                    .ToList<User>();
            }
            return managers;
        }
        /// <summary>
        /// Достаем подразделение по коду
        /// </summary>
        /// <param name="Code">КодС</param>
        /// <returns></returns>
        public Department GetByCode(string Code)
        {
            return (Department)Session.CreateCriteria(typeof(Department))
                   .Add(Restrictions.Eq("Code", Code)).UniqueResult();
        }
        /// <summary>
        /// Достаем уровень подразделений из СКД с привязкой к точкам из Финграда по заданному родителю.
        /// </summary>
        /// <param name="DepId">Код родительского подразделения.</param>
        /// <returns></returns>
        public IList<DepartmentWithFigradPointsDto> GetDepartmentWithFingradPoint(string DepId)
        {
            const string sqlQuery = (@"SELECT A.Id, A.Version, A.Code, A.Name, A.Code1C, A.ParentId, A.Path, A.ItemLevel, A.CodeSKD, A.Priority,
                                              B.AvailableEmployees, B.FinDepNameShort, B.FinDepPointCode, B.FinDepName, B.FinDepCode, B.FinAdminCode, B.FinBGCode, B.RPLinkCode, B.ComplianceSign
                                       FROM Department as A
                                       LEFT JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
                                       WHERE ParentId = :DepId");
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Version", NHibernateUtil.Int32).
                AddScalar("Code", NHibernateUtil.String).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Code1C", NHibernateUtil.Int32).
                AddScalar("ParentId", NHibernateUtil.Int32).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32).
                AddScalar("CodeSKD", NHibernateUtil.String).
                AddScalar("Priority", NHibernateUtil.Int32).
                AddScalar("AvailableEmployees", NHibernateUtil.String).
                AddScalar("FinDepNameShort", NHibernateUtil.String).
                AddScalar("FinDepPointCode", NHibernateUtil.String).
                AddScalar("FinDepName", NHibernateUtil.String).
                AddScalar("FinDepCode", NHibernateUtil.String).
                AddScalar("FinAdminCode", NHibernateUtil.String).
                AddScalar("FinBGCode", NHibernateUtil.String).
                AddScalar("RPLinkCode", NHibernateUtil.String).
                AddScalar("ComplianceSign", NHibernateUtil.String).
                SetString("DepId", DepId).
                SetResultTransformer(Transformers.AliasToBean(typeof(DepartmentWithFigradPointsDto))).
                List<DepartmentWithFigradPointsDto>();
        }
        /// <summary>
        /// Подсчет количества штатных единиц в пределах указанного подразделения.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        public int DepPositionCount(int Id)
        {
            return Session.CreateSQLQuery(@"SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(:Id) as SEPCount")
                .AddScalar("SEPCount", NHibernateUtil.Int32)
                .SetInt32("Id", Id)
                .UniqueResult<int>();
        }

        /// <summary>
        /// Название подразделения из Финграда.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        public string DepFingradName(int Id)
        {
            return Session.CreateSQLQuery(@"SELECT B.NameShort
                                            FROM StaffDepartmentRequest as A
                                            INNER JOIN StaffDepartmentManagerDetails as B ON B.DepRequestId = A.Id
                                            WHERE A.IsUsed = 1 and A.Id = :Id")
                .AddScalar("NameShort", NHibernateUtil.String)
                .SetInt32("Id", Id)
                .UniqueResult<string>();
        }
    }
}