using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник оснований для замещения сотрудников
    /// </summary>
    public class StaffReplacementReasonsDao : DefaultDao<StaffReplacementReasons>, IStaffReplacementReasonsDao
    {
        public StaffReplacementReasonsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
