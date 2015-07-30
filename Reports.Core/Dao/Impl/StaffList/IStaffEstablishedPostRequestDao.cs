using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Заявки по штатным единицам.
    /// </summary>
    public interface IStaffEstablishedPostRequestDao : IDao<StaffEstablishedPostRequest>
    {
        /// <summary>
        /// Достаем Id действующей заявки для данной штатной единицы.
        /// </summary>
        /// <param name="Id">Id штатной единицы.</param>
        /// <returns></returns>
        int GetCurrentRequestId(int SEPId);
    }
}
