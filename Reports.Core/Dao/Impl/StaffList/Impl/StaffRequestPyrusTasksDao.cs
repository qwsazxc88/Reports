using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;


namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Задачи в Пайрусе.
    /// </summary>
    public class StaffRequestPyrusTasksDao : DefaultDao<StaffRequestPyrusTasks>, IStaffRequestPyrusTasksDao
    {
        public StaffRequestPyrusTasksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
