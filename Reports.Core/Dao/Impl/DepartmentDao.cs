﻿using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;

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
    }
}