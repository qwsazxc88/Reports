using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionOrderRoleRecordDao : DefaultDao<MissionOrderRoleRecord>, IMissionOrderRoleRecordDao
    {
        public MissionOrderRoleRecordDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual List<Department> LoadDepartmentsForUserId(int userId)
        {
            return Session.Query<MissionOrderRoleRecord>()
                .Where(x => x.Role.Id == 1 && x.TargetDepartment != null && x.User.Id == userId)
                .Select(x => x.TargetDepartment).Distinct()
                .ToList();
        }
    }
}