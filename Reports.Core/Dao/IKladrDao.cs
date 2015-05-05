using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IKladrDao : IDao<KladrDto>
    {
        /// <summary>
        /// Достаем список регионов из базы данных.
        /// </summary>
        /// <returns></returns>
        IList<KladrDto> GetRegions();
        /// <summary>
        /// Достаем список райнонов.
        /// </summary>
        /// <param name="RegionCode">Код региона.</param>
        /// <returns></returns>
        IList<KladrDto> GetAreas(string RegionCode);
        /// <summary>
        /// Достаем список городов.
        /// </summary>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <returns></returns>
        IList<KladrDto> GetCityes(string RegionCode, string AreaCode);
        /// <summary>
        /// Достаем список населенных пунктов.
        /// </summary>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <param name="CityCode">Код города.</param>
        /// <returns></returns>
        IList<KladrDto> GetSettlements(string RegionCode, string AreaCode, string CityCode);
    }
}
