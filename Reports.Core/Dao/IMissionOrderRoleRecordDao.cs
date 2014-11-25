using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionOrderRoleRecordDao : IDao<MissionOrderRoleRecord>
    {
        IList<MissionOrderRoleRecord> GetRoleRecords(User user = null, string roleCode = null, User targetUser = null, Department targetDepartment = null, bool allLevels = false);
    }
}
