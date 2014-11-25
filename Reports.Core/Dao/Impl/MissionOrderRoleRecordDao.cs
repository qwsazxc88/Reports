﻿using System.Collections.Generic;
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

        public IList<MissionOrderRoleRecord> GetRoleRecords(User user = null, string roleCode = null, User targetUser = null, Department targetDepartment = null, bool allLevels = false)
        {
            var result = Session.Query<MissionOrderRoleRecord>().ToList<MissionOrderRoleRecord>();
            result = result.Where(morr => true
                && (user != null ? user.Id == morr.User.Id : true)
                && (roleCode != null ? roleCode == morr.Role.Code : true)
                && (targetUser != null && morr.TargetUser != null ? targetUser.Id == morr.TargetUser.Id : true)
                && (targetDepartment != null && morr.TargetDepartment != null ?
                    targetDepartment.Id == morr.TargetDepartment.Id
                        // Если необходимо, включить записи вышележащих подразделений
                        || (allLevels ? targetDepartment.Path.Contains(morr.TargetDepartment.Path) : false) : true)
                ).ToList<MissionOrderRoleRecord>();

            return result;
        }
    }
}