﻿using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Reports.Core.Dao.Impl
{
    public class ManualRoleRecordDao : DefaultDao<ManualRoleRecord>, IManualRoleRecordDao
    {
        public ManualRoleRecordDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<ManualRoleRecord> GetRoleRecords(User user = null, string roleCode = null, User targetUser = null, Department targetDepartment = null)
        {
            var result = Session.Query<ManualRoleRecord>().ToList<ManualRoleRecord>();
            result = result.Where(mrr => true
                && (user != null ? user.Id == mrr.User.Id : true)
                && (roleCode != null ? roleCode == mrr.Role.Code : true)
                && (targetUser != null && mrr.TargetUser != null ? targetUser.Id == mrr.TargetUser.Id : true)
                && (targetDepartment != null && mrr.TargetDepartment != null ? targetDepartment.Id == mrr.TargetDepartment.Id : true)
                ).ToList<ManualRoleRecord>();

            return result;
        }
    }
}