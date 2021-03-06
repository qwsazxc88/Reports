﻿using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TerraPointDao : DefaultDao<TerraPoint>, ITerraPointDao
    {
        public TerraPointDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<TerraPoint> GetTerraPointTree(int tpId)
        {
            const string sqlQuery = (@";with Parents as
                (select d1.Id,d1.Name
                ,d1.parentId
                ,d1.path, d1.itemlevel from dbo.TerraPoint d
                inner join dbo.TerraPoint d1 on d.Path like d1.Path+'%' 
                where d.Id = :tpId)
                select d1.Id, d1.Version, d1.Code, d1.Name,d1.ShortName, d1.Code1C
                , d1.ParentId, d1.Path, d1.ItemLevel, d1.EndDate
                from dbo.TerraPoint d1
                inner join Parents p on d1.path like p.path+'%'
                and d1.ItemLevel = p.ItemLevel+1
                order by d1.itemlevel 
                ");
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Version", NHibernateUtil.Int32).
                AddScalar("Code", NHibernateUtil.String).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("ShortName", NHibernateUtil.String).
                AddScalar("Code1C", NHibernateUtil.String).
                AddScalar("ParentId", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                SetInt32("tpId", tpId).
                SetResultTransformer(Transformers.AliasToBean(typeof(Department))).
                List<TerraPoint>();
        }
        public virtual IList<TerraPoint> FindByLevelAndParentId(int level,string parentId)
        {
            if(string.IsNullOrEmpty(parentId))
                return Session.CreateCriteria(typeof(TerraPoint)).Add(Restrictions.Eq("ItemLevel", level)).List<TerraPoint>();
            return
                Session.CreateCriteria(typeof(TerraPoint))
                .Add(Restrictions.And(
                    Restrictions.Eq("ItemLevel", level),
                    Restrictions.Eq("ParentId", parentId)
                ))
                .AddOrder( new Order("Name",true))
                .List<TerraPoint>();
        }
        public virtual TerraPoint FindByCode1C(string code1C)
        {
            return (TerraPoint)Session.CreateCriteria(typeof(TerraPoint))
                .Add(Restrictions.Eq("Code1C", code1C)).UniqueResult();
        }
        public virtual IdNameDto FindByCode1CAndPath(string code1C, string path)
        {
            const string sqlQuery = @"select id as Id,parentId as Name from [dbo].[TerraPoint]
                                    where Code1c = :code1c
                                    /*and :path like [Path]+N'%'*/";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                               AddScalar("Id", NHibernateUtil.Int32).
                               AddScalar("Name", NHibernateUtil.String).
                               SetString("code1c", code1C);
                               //SetString("path", path);   //убрал из запроса условия для пути
            //query.SetDateTime("endDate", endDate);
            return (IdNameDto)query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).UniqueResult(); ;
            //return (TerraPoint)Session.CreateCriteria(typeof(TerraPoint))
            //    .Add(Restrictions.Eq("Code1C", code1C))
            //    .Add(Restrictions.Like("path", path))
            //    .UniqueResult();
        }
    }
}