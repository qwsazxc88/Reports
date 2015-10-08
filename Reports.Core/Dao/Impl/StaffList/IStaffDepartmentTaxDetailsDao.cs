using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Налоговые реквизиты.
    /// </summary>
    public interface IStaffDepartmentTaxDetailsDao : IDao<StaffDepartmentTaxDetails>
    {
        /// <summary>
        /// Достаем налоговые реквизиты по идентификатору подразделения.
        /// </summary>
        /// <param name="Dep">Подразделение.</param>
        /// <returns></returns>
        StaffDepartmentTaxDetails GetDetailsByDepartmentId(Department dep);
    }
}
