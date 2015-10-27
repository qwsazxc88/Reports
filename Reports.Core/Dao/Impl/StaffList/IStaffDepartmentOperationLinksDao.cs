using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Операции проводимые подразделением.
    /// </summary>
    public interface IStaffDepartmentOperationLinksDao : IDao<StaffDepartmentOperationLinks>
    {
        /// <summary>
        /// Список связей с операциями.
        /// </summary>
        /// <param name="OperGroupId">Id группы операций.</param>
        /// <returns></returns>
        IList<StaffDepartmentOperationLinksDto> GetOperationGroupLinks(int OperGroupId);
    }
}
