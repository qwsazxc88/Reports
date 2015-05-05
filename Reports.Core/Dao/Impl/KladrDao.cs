using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;

namespace Reports.Core.Dao.Impl
{
    public class KladrDao : DefaultDao<KladrDto>, IKladrDao
    {
        public KladrDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Достаем список регионов из базы данных.
        /// </summary>
        /// <returns></returns>
        public IList<KladrDto> GetRegions()
        {
            IQuery query = CreateRegionQuery(@"SELECT Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code FROM dbo.vwKladrRegions
                                         ORDER BY Name");

            return query.SetResultTransformer(Transformers.AliasToBean<KladrDto>()).List<KladrDto>();
        }

        public IQuery CreateRegionQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("ShortName", NHibernateUtil.String)
                .AddScalar("Index", NHibernateUtil.String)
                .AddScalar("AltName", NHibernateUtil.String)
                .AddScalar("AddressType", NHibernateUtil.String)
                .AddScalar("RegionCode", NHibernateUtil.String)
                .AddScalar("AreaCode", NHibernateUtil.String)
                .AddScalar("CityCode", NHibernateUtil.String)
                .AddScalar("SettlementCode", NHibernateUtil.String)
                .AddScalar("StreetCode", NHibernateUtil.String)
                .AddScalar("Code", NHibernateUtil.String)
                ;
            return query;
        }
    }
}
