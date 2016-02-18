﻿using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IManualRoleRecordDao : IDao<ManualRoleRecord>
    {
        IList<ManualRoleRecord> GetRoleRecords(User user = null, string roleCode = null, User targetUser = null, Department targetDepartment = null);
        IList<Department> LoadDepartmentsForUserId(int userId, List<int> roleIds=null);
        IList<User> GetManualRoleHoldersForUser(User user, UserManualRole role);
        IList<User> GetManualRoleHoldersForUser(int userId, UserManualRole role);
    }
}
