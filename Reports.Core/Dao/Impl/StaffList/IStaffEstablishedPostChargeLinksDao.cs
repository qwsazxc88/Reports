using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Связь надбавок с штатными единицами.
    /// </summary>
    public interface IStaffEstablishedPostChargeLinksDao : IDao<StaffEstablishedPostChargeLinks>
    {
        /// <summary>
        /// Достаем список надбавок для заявки к штатной единице.
        /// </summary>
        /// <param name="Id">Id заявки штатного расписания.</param>
        /// <returns></returns>
        IList<StaffEstablishedPostChargeLinksDto> GetChargesForRequests(int Id);
        /// <summary>
        /// Достаем список надбавок для штатной единицы.
        /// </summary>
        /// <param name="Id">Id штатной единицы.</param>
        /// <returns></returns>
        IList<StaffEstablishedPostChargeLinksDto> GetChargesForEstablishedPosts(int Id);
    }
}
