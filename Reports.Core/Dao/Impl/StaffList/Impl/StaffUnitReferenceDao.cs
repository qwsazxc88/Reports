using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник единиц измерения
    /// </summary>
    public class StaffUnitReferenceDao : DefaultDao<StaffUnitReference>, IStaffUnitReferenceDao
    {
        public StaffUnitReferenceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
