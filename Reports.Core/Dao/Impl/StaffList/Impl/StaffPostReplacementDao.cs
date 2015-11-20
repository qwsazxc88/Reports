using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Журнал замещения сотрудников.
    /// </summary>
    public class StaffPostReplacementDao : DefaultDao<StaffPostReplacement>, IStaffPostReplacementDao
    {
        public StaffPostReplacementDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
