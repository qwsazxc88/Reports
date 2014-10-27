using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class TerraGraphicDao : DefaultDao<TerraGraphic>, ITerraGraphicDao
    {
        public TerraGraphicDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<TerraGraphic> LoadForIdsList(List<int> userIds,
                                                   int month, int year)
        {
            if (userIds.Count == 0)
                return new List<TerraGraphic>();
            var beginDate = new DateTime(year, month, 1);
            DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
            ICriteria criteria = Session.CreateCriteria(typeof(TerraGraphic));
            criteria.Add(Restrictions.In("UserId", userIds))
                .Add(Restrictions.Between("Day", beginDate, endDate));
            return criteria.List<TerraGraphic>();
        }
        public IList<TerraGraphicDbDto> LoadDtoForIdsList(List<int> userIds,int month, int year)
        {
            var beginDate = new DateTime(year, month, 1);
            DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
            return LoadDtoForIdsList(userIds,beginDate,endDate);
        }
        public IList<TerraGraphicDbDto> LoadDtoForIdsList(List<int> userIds,DateTime beginDate,DateTime endDate)
        {
         
            if (userIds.Count == 0)
                return new List<TerraGraphicDbDto>();
           
            string sqlQuery = @"select tg.Id
                                ,UserId
                                ,Day
                                ,Hours
                                ,PointId
                                ,FactHours
                                ,FactPointId
                                ,IsCreditAvailable
                                ,IsFactCreditAvailable
                                ,case when tp.Id is not null then tp.ShortName else N'' end  as PointName
                                ,case when tp.Id is not null then tp.Name else N'' end  as PointTitle
                                ,case when tpf.Id is not null then tpf.ShortName else N'' end  as FactPointName
                                ,case when tpf.Id is not null then tpf.Name else N'' end  as FactPointTitle
                                from dbo.TerraGraphic tg
                                left join dbo.TerraPoint tp on tp.Id = tg.PointId
                                left join dbo.TerraPoint tpf on tpf.Id = tg.FactPointId
                              where 
                          tg.Day between :beginDate and :endDate
                          and tg.UserId in (:userList)";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
              AddScalar("Id", NHibernateUtil.Int32).
              AddScalar("UserId", NHibernateUtil.Int32).
              AddScalar("Day", NHibernateUtil.DateTime).
              AddScalar("Hours", NHibernateUtil.Decimal).
              AddScalar("PointId", NHibernateUtil.Int32).
              AddScalar("FactHours", NHibernateUtil.Decimal).
              AddScalar("FactPointId", NHibernateUtil.Int32).
              AddScalar("IsCreditAvailable", NHibernateUtil.Boolean).
              AddScalar("IsFactCreditAvailable", NHibernateUtil.Boolean).
              AddScalar("PointName", NHibernateUtil.String).
              AddScalar("PointTitle", NHibernateUtil.String).
              AddScalar("FactPointName", NHibernateUtil.String).
              AddScalar("FactPointTitle", NHibernateUtil.String).
              SetDateTime("beginDate", beginDate).
              SetDateTime("endDate", endDate).
              SetParameterList("userList", userIds);
            //SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(TerraGraphicDbDto))).List<TerraGraphicDbDto>();
        }
    }
}