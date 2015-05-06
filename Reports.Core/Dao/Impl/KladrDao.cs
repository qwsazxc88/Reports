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
        /// Достаем запись справочика по коду.
        /// </summary>
        /// <param name="Code">Код записи.</param>
        /// <returns></returns>
        public IList<KladrDto> GetKladrByCode(string Code)
        {
            IQuery query = CreateKladrQuery(string.Format(@"SELECT top 1 Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code FROM dbo.Kladr {0}",
                string.IsNullOrEmpty(Code) ? "WHERE Code is null" : " WHERE Code = '" + Code + "' "));

            return query.SetResultTransformer(Transformers.AliasToBean<KladrDto>()).List<KladrDto>();
        }

        /// <summary>
        /// Достаем список регионов из базы данных.
        /// </summary>
        /// <returns></returns>
        public IList<KladrDto> GetRegions()
        {
            IQuery query = CreateKladrQuery(@"SELECT Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code FROM dbo.vwKladrRegions
                                         ORDER BY Name");

            return query.SetResultTransformer(Transformers.AliasToBean<KladrDto>()).List<KladrDto>();
        }
        /// <summary>
        /// Достаем список райнонов.
        /// </summary>
        /// <param name="RegionCode">Код региона.</param>
        /// <returns></returns>
        public IList<KladrDto> GetAreas(string RegionCode)
        {
            IQuery query = CreateKladrQuery(string.Format(@"SELECT Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code FROM dbo.vwKladrAreas {0} ORDER BY Name", 
                string.IsNullOrEmpty(RegionCode) ? "WHERE RegionCode is null" : " WHERE RegionCode = '" + RegionCode + "' "));

            return query.SetResultTransformer(Transformers.AliasToBean<KladrDto>()).List<KladrDto>();
        }

        /// <summary>
        /// Достаем список городов.
        /// </summary>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <returns></returns>
        public IList<KladrDto> GetCityes(string RegionCode, string AreaCode)
        {
            IQuery query = CreateKladrQuery(string.Format(@"SELECT Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code FROM dbo.vwKladrCityes {0} {1} ORDER BY Name",
                string.IsNullOrEmpty(RegionCode) ? "WHERE RegionCode is null" : " WHERE RegionCode = '" + RegionCode + "' ",
                string.IsNullOrEmpty(RegionCode) ? "and AreaCode is null" : " and AreaCode = '" + AreaCode + "' "));

            return query.SetResultTransformer(Transformers.AliasToBean<KladrDto>()).List<KladrDto>();
        }

        /// <summary>
        /// Достаем список населенных пунктов.
        /// </summary>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <param name="CityCode">Код города.</param>
        /// <returns></returns>
        public IList<KladrDto> GetSettlements(string RegionCode, string AreaCode, string CityCode)
        {
            IQuery query = CreateKladrQuery(string.Format(@"SELECT Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code FROM dbo.vwKladrCityes {0} {1} ORDER BY Name",
                string.IsNullOrEmpty(RegionCode) ? "WHERE RegionCode is null" : " WHERE RegionCode = '" + RegionCode + "' ",
                string.IsNullOrEmpty(RegionCode) ? "and AreaCode is null" : " and AreaCode = '" + AreaCode + "' "));

            return query.SetResultTransformer(Transformers.AliasToBean<KladrDto>()).List<KladrDto>();
        }

        public IQuery CreateKladrQuery(string sqlQuery)
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
