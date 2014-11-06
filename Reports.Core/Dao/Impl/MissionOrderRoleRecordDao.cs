using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Reports.Core.Dao.Impl
{
    public class MissionOrderRoleRecordDao : DefaultDao<MissionOrderRoleRecord>, IMissionOrderRoleRecordDao
    {
        public MissionOrderRoleRecordDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<MissionOrderRoleRecord> GetRoleRecords(User user = null, string roleCode = null, User targetUser = null, Department targetDepartment = null)
        {
            return Session.Query<MissionOrderRoleRecord>().Where(morr => true
                && (user != null ? user.Id == morr.User.Id : true)
                && (roleCode != null ? roleCode == morr.Role.Code : true)
                && (targetUser != null ? targetUser.Id == morr.TargetUser.Id : true)
                && (targetDepartment != null ? targetDepartment.Id == morr.TargetDepartment.Id : true)
                ).ToList<MissionOrderRoleRecord>();
        }
    }
}