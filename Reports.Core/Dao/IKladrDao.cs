using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IKladrDao : IDao<KladrDto>
    {
        /// <summary>
        /// Достаем запись справочика по коду.
        /// </summary>
        /// <param name="Code">Код записи.</param>
        /// <returns></returns>
        IList<KladrDto> GetKladrByCode(string Code);
        /// <summary>
        /// Достаем список из базы данных.
        /// </summary>
        /// <param name="AddressType">Тип записи.</param>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <param name="CityCode">Код города.</param>
        /// <param name="SettlementCode">Код населенного пункта.</param>
        /// <returns></returns>
        IList<KladrDto> GetKladr(int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode);
    }
}
