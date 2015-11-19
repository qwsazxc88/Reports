using Reports.Core.Domain;
using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник видов идентификаций сетевых магазинов
    /// </summary>
    public interface IStaffNetShopIdentificationDao : IDao<StaffNetShopIdentification>
    {
        /// <summary>
        /// Список причин занесения в справочник подразделений.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetNetShopTypes();
    }
}
