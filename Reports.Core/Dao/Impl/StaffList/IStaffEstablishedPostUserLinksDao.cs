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
    }
}
