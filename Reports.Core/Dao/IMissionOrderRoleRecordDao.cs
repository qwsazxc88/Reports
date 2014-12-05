using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IMissionOrderRoleRecordDao : IDao<MissionOrderRoleRecord>
    {
        List<Department> LoadDepartmentsForUserId(int userId);
    }
}