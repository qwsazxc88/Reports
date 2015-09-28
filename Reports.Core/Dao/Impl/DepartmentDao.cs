﻿using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
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
        public IList<Reports.Core.Dto.Terrapoint_DepartmentDto> GetTP_D_list()
        {
            var querystr = "SELECT * FROM vwTerrapoint_Department";
            var query = Session.CreateSQLQuery(querystr)
                  .AddScalar("TPCode", NHibernateUtil.String)
                  .AddScalar("TPName", NHibernateUtil.String)
                  .AddScalar("TPCity", NHibernateUtil.String)
                  .AddScalar("TPStreet", NHibernateUtil.String)
                  .AddScalar("Did", NHibernateUtil.Int32)
                  .AddScalar("Dcode", NHibernateUtil.String)
                  .AddScalar("Dname", NHibernateUtil.String)
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
              .AddScalar("D3name", NHibernateUtil.String)
              .AddScalar("Dcity", NHibernateUtil.String)
              .AddScalar("Dstreet", NHibernateUtil.String)
              .AddScalar("Dhouse", NHibernateUtil.String)
              .AddScalar("TPcode", NHibernateUtil.String)
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
    }
}