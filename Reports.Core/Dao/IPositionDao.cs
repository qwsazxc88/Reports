using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IPositionDao : IDao<Position>
    {
        /// <summary>
        /// Должности.
        /// </summary>
        /// <param name="Term">фрагмент названия для поиска.</param>
        /// <returns></returns>
        IList<IdNameDto> GetPositions(string Term);

        /// <summary>
        /// Действующие должности.
        /// </summary>
        /// <param name="Term">фрагмент названия для поиска.</param>
        /// <returns></returns>
        IList<IdNameDto> GetOperatingPositions(string Term);
    }
}