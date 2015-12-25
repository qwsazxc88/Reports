using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Связи штатных единиц с сотрудниками (штатная расстановка)
    /// </summary>
    public interface IStaffEstablishedPostUserLinksDao : IDao<StaffEstablishedPostUserLinks>
    {
        /// <summary>
        /// Достаем строку штатной расстановки по привязке к документу.
        /// </summary>
        /// <param name="DocId">Id документа.</param>
        /// <param name="ReserveType">Тип резервирования (StaffReserveTypeEnum)</param>
        /// <returns></returns>
        StaffEstablishedPostUserLinks GetPostUserLinkByDocId(int DocId, int ReserveType);
    }
}
