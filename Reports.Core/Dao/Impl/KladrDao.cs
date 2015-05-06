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
        /// Достаем список из базы данных.
        /// </summary>
        /// <param name="AddressType">Тип записи.</param>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <param name="CityCode">Код города.</param>
        /// <param name="SettlementCode">Код населенного пункта.</param>
        /// <returns></returns>
        public IList<KladrDto> GetKladr(int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode)
        {
            string View = string.Empty;
            string QueryWhere = string.Empty;
            switch (AddressType)
            {
                case 1:
                    View = "dbo.vwKladrRegions";
                    break;
                case 2:
                    View = "dbo.vwKladrAreas";
                    QueryWhere = string.IsNullOrEmpty(RegionCode) ? "WHERE RegionCode is null " : " WHERE RegionCode = '" + RegionCode + "' ";
                    break;
                case 3:
                    View = "dbo.vwKladrCityes";
                    QueryWhere = (string.IsNullOrEmpty(RegionCode) ? "WHERE RegionCode is null " : " WHERE RegionCode = '" + RegionCode + "' ") + 
                        (QueryWhere = string.IsNullOrEmpty(AreaCode) ? "and AreaCode is null " : " and AreaCode = '" + AreaCode + "' ");
                    break;
                case 4:
                    View = "dbo.vwKladrSettlements";
                    QueryWhere = (string.IsNullOrEmpty(RegionCode) ? "WHERE RegionCode is null " : " WHERE RegionCode = '" + RegionCode + "' ") +
                        (QueryWhere = string.IsNullOrEmpty(AreaCode) ? "and AreaCode is null " : " and AreaCode = '" + AreaCode + "' ") +
                        (QueryWhere = string.IsNullOrEmpty(CityCode) ? "and CityCode is null " : " and CityCode = '" + CityCode + "' ");
                    break;
                case 5:
                    View = "dbo.vwKladrStreets";
                    QueryWhere = (string.IsNullOrEmpty(RegionCode) ? "WHERE RegionCode is null " : " WHERE RegionCode = '" + RegionCode + "' ") +
                        (QueryWhere = string.IsNullOrEmpty(AreaCode) ? "and AreaCode is null " : " and AreaCode = '" + AreaCode + "' ") +
                        (QueryWhere = string.IsNullOrEmpty(CityCode) ? "and CityCode is null " : " and CityCode = '" + CityCode + "' ") +
                        (QueryWhere = string.IsNullOrEmpty(SettlementCode) ? "and SettlementCode is null " : " and SettlementCode = '" + SettlementCode + "' ");
                    break;
            }



            IQuery query = CreateKladrQuery(string.Format(@"SELECT Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code FROM {0} {1} ORDER BY Name", View, QueryWhere));

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
