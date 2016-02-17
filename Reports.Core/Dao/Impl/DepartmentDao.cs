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
        public IList<Department> GetChildDepartments(Department dep)
        {
            return Session.CreateCriteria<Department>()
                .Add(Expression.Like("Path", dep.Path + "%"))
                .Add(Expression.Not(Expression.Eq("Id", dep.Id)))
                .List<Department>();
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
        public DepartmentTreeDto GetDepartmentTreeDto()
        {
            System.Collections.Hashtable table = new System.Collections.Hashtable();
            string sql = @"SELECT id,ParentId,Name as text FROM Department
                            order by ItemLevel,Id";
            var result = Session.CreateSQLQuery(sql)
                .AddScalar("id", NHibernateUtil.Int32)
                .AddScalar("ParentId", NHibernateUtil.Int32)
                .AddScalar("text", NHibernateUtil.String)
                .SetResultTransformer(Transformers.AliasToBean<DepartmentTreeDto>()).List<DepartmentTreeDto>();
            var first = result.First();
            foreach(var el in result)
            {
                table.Add(el.id, el);
            }
            foreach (var el in result)
            {
                if (el.ParentId > 0)
                {
                    ((DepartmentTreeDto)table[el.ParentId]).children.Add(el);
                }
            }
            return first;

        }
        public IList<Reports.Core.Dto.Terrapoint_DepartmentDto> GetTP_D_list()
        {
            var querystr = "SELECT * FROM vwTerrapoint_Department";
            var query = Session.CreateSQLQuery(querystr)
                  .AddScalar("TPCode", NHibernateUtil.String)
                  .AddScalar("TP1Name", NHibernateUtil.String)
                  .AddScalar("TP2Name", NHibernateUtil.String)
                  .AddScalar("TPName", NHibernateUtil.String)
                  .AddScalar("TPCity", NHibernateUtil.String)
                  .AddScalar("TPStreet", NHibernateUtil.String)
                  .AddScalar("Did", NHibernateUtil.Int32)
                  .AddScalar("Dcode", NHibernateUtil.String)
                  .AddScalar("Dname", NHibernateUtil.String)
                  .AddScalar("D2name", NHibernateUtil.String)
                  .AddScalar("D3name", NHibernateUtil.String)
                  .AddScalar("D4name", NHibernateUtil.String)
                  .AddScalar("D5name", NHibernateUtil.String)
                  .AddScalar("D6name", NHibernateUtil.String)
                  .AddScalar("BFGId", NHibernateUtil.String)
                  .AddScalar("Dcity", NHibernateUtil.String)
                  .AddScalar("Dstreet", NHibernateUtil.String)
                  .AddScalar("Dhouse", NHibernateUtil.String)
                  .AddScalar("D1id", NHibernateUtil.Int32)
                  .AddScalar("D1code", NHibernateUtil.String)
                  .AddScalar("D1name", NHibernateUtil.String)
                  .AddScalar("D1city", NHibernateUtil.String)
                  .AddScalar("D1street", NHibernateUtil.String)
                  .AddScalar("D1house", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(Reports.Core.Dto.Terrapoint_DepartmentDto))).List<Reports.Core.Dto.Terrapoint_DepartmentDto>();
        }
        public IList<Reports.Core.Dto.Department_TerrapointDto> GetD_TP_list()
        {
            var querystr = "SELECT * FROM vwDepartment_Terrapoint";
            var query = Session.CreateSQLQuery(querystr)
                .AddScalar("Did", NHibernateUtil.Int32)
              .AddScalar("Dcode", NHibernateUtil.String)
              .AddScalar("Dname", NHibernateUtil.String)
              .AddScalar("D2name", NHibernateUtil.String)
              .AddScalar("D3name", NHibernateUtil.String)
              .AddScalar("D4name", NHibernateUtil.String)
              .AddScalar("D5name", NHibernateUtil.String)
              .AddScalar("D6name", NHibernateUtil.String)
              .AddScalar("BFGId", NHibernateUtil.String)
              .AddScalar("Dcity", NHibernateUtil.String)
              .AddScalar("Dstreet", NHibernateUtil.String)
              .AddScalar("Dhouse", NHibernateUtil.String)
              .AddScalar("TPcode", NHibernateUtil.String)
              .AddScalar("TP1Name", NHibernateUtil.String)
              .AddScalar("TP2Name", NHibernateUtil.String)
              .AddScalar("TPName", NHibernateUtil.String)
              .AddScalar("TPcity", NHibernateUtil.String)
              .AddScalar("TPstreet", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(Reports.Core.Dto.Department_TerrapointDto))).List<Reports.Core.Dto.Department_TerrapointDto>();
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
        public IList<User> GetDepartmentManagersByDepAndManualRole(int departmentId, int RoleId)
        {
            var strquery = string.Format("exec spGetManagersForDepartmentWithManualRole {0},{1}", departmentId, RoleId);
            var query = Session.CreateSQLQuery(strquery).AddEntity(typeof(User));
            IList<User> result = query.List<User>();
            return result;
        }
        /// <summary>
        /// Определяем руководителей для подразделения по автоматическим правам и ручным привязкам.
        /// </summary>
        /// <param name="departmentId">Id подразделения для которго ищем руководителей</param>
        /// <returns></returns>
        public IList<User> GetDepartmentManagersWithManualLinks(int departmentId)
        {
            IList<User> managers;
            Department department = Get(departmentId);

            int BFGID = department.DepartmentAccessory == null ? 0 : department.DepartmentAccessory.Id;

            if (department == null)
            {
                return new List<User>();
            }
            //автоматические права
            managers = Session.Query<User>()
                .Where<User>(user => (user.RoleId & (int)UserRole.Manager) > 0 && department.Path.StartsWith(user.Department.Path) && user.IsActive == true)
                .OrderByDescending(x => x.Level)
                .ToList<User>();


            //ручные привязки с учетом принадлежности подразделения (бэк/фронт)
            IList<User> ManualManagers = Session.Query<ManualRoleRecord>()
                .Where(x => x.Role.Id == (BFGID != 0 && BFGID != 6 ? BFGID : x.Role.Id) && x.TargetDepartment != null && department.Path.StartsWith(x.TargetDepartment.Path))
                .Select(x => x.User)
                .OrderByDescending(x => x.Level).Distinct()
                .ToList();

            IList<User> ManagerList = new List<User>().Union(managers).Union(ManualManagers).ToList();

            return ManagerList;
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
        /// Достаем уровень подразделений с полями из финграда.
        /// </summary>
        /// <param name="Id">Id родительского подразделения</param>
        /// <param name="IsParentDepOnly">Признак достать только родительское подразделение.</param>
        /// <param name="role">Роль текущего пользователя</param>
        /// <param name="UserId">Id сотрудника</param>
        /// <param name="IsBegin"></param>
        /// <returns></returns>
        public IList<StaffListDepartmentDto> DepFingradName(string Id, bool IsParentDepOnly, UserRole role, int UserId, bool IsBegin)
        {
            string SqlQuery = @"SELECT A.Id, Code, Name, Code1C, ParentId, Path, A.ItemLevel, CodeSKD, Priority, DepFingradName, DepFingradNameComment, FinDepPointCode, dbo.fnGetStaffEstablishedPostCountByDepartment(A.Id) as SEPCount
                                FROM vwStaffListDepartment as A ";

            string SqlWhere = string.Empty;
            if ((role & UserRole.Manager) > 0 && IsBegin)
                SqlQuery += @"INNER JOIN dbo.fnGetBeginDeparmentForUser(" + UserId.ToString() + ") as B ON B.Id = A.Id ";
            else
                SqlWhere = (!IsParentDepOnly ? (string.IsNullOrEmpty(Id) ? "A.ParentId is null" : "A.ParentId = " + Id) : "A.Code1C = " + Id);

            //string SqlWhere = (!IsParentDepOnly ? (string.IsNullOrEmpty(Id) ? "A.ParentId is null" : "A.ParentId = " + Id) : "A.Code1C = " + Id);
            //string SqlWhere = (!IsParentDepOnly ? (string.IsNullOrEmpty(Id) ? "A.ParentId is null" : "A.ParentId = " + Id) : "");




            SqlWhere += (role == UserRole.Inspector ? (string.IsNullOrEmpty(SqlWhere) && string.IsNullOrWhiteSpace(SqlWhere) ? "" : " and ") + " (BFGId in (2, 6) or BFGId is null) " : "");
            SqlWhere += (string.IsNullOrEmpty(SqlWhere) && string.IsNullOrWhiteSpace(SqlWhere) ? "" : " and ") + " isnull(BFGId, 0) not in (3, 5)";
//            return Session.CreateSQLQuery(string.Format(@"SELECT Id, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, DepFingradName, DepFingradNameComment, FinDepPointCode, dbo.fnGetStaffEstablishedPostCountByDepartment(A.Id) as SEPCount
//                                            FROM vwStaffListDepartment as A
//                                            WHERE {0} ORDER BY Priority, Name", SqlWhere))
            SqlWhere = string.Format(@" WHERE {0} ORDER BY Priority, Name", SqlWhere);
            return Session.CreateSQLQuery(SqlQuery + SqlWhere)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("Code1C", NHibernateUtil.Int32)
                .AddScalar("ParentId", NHibernateUtil.Int32)
                .AddScalar("Path", NHibernateUtil.String)
                .AddScalar("ItemLevel", NHibernateUtil.Int32)
                .AddScalar("CodeSKD", NHibernateUtil.String)
                .AddScalar("Priority", NHibernateUtil.Int32)
                .AddScalar("DepFingradName", NHibernateUtil.String)
                .AddScalar("DepFingradNameComment", NHibernateUtil.String)
                .AddScalar("FinDepPointCode", NHibernateUtil.String)
                .AddScalar("SEPCount", NHibernateUtil.Int32)
                .SetResultTransformer(Transformers.AliasToBean(typeof(StaffListDepartmentDto))).List<StaffListDepartmentDto>();
        }
    }
}