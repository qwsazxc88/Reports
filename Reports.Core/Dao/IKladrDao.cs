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
    }
}
