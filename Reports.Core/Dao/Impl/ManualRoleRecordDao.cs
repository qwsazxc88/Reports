﻿using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;

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

        public virtual IList<Department> LoadDepartmentsForUserId(int userId, List<int> roleIds=null)
        {
            //Добавил дополнительный параметр для ручных привязок, чтобы учитывались роли.
            if (roleIds == null) roleIds = new List<int>() { 1 };
            return Session.Query<ManualRoleRecord>()
                .Where(x => roleIds.Contains(x.Role.Id) && x.TargetDepartment != null && x.User.Id == userId && x.TargetDepartment.ItemLevel == 3)
                .Select(x => x.TargetDepartment).Distinct()
                .ToList();
        }

        public virtual IList<User> GetManualRoleHoldersForUser(int userId, UserManualRole role)
        {
            #region Query string
            const string sqlQuery = @"
                SELECT DISTINCT manager.*
                FROM Users manager
                INNER JOIN Users employee ON employee.Id = :userId
                INNER JOIN ManualRoleRecord mrr
	            ON
	            (
		            -- Выбираемый руководитель является держателем роли
		            mrr.UserId = manager.Id
		            AND
		            mrr.RoleId = :roleId
		            AND
		            (
			            -- Объектом роли является проверяемый сотрудник
			            mrr.TargetUserId = :userId
			            OR
			            -- Объектом роли является любое подразделение,
			            mrr.TargetDepartmentId IN
			            (
				            SELECT managerDept.Id
				            FROM Department managerDept
				            INNER JOIN Department employeeDept
					        ON
					        (
						        -- в ветке которого есть подразделение,
						        employeeDept.Path LIKE managerDept.Path + N'%'
						        AND
						        -- являющееся подразделением проверяемого сотрудника
						        employeeDept.Id = employee.DepartmentId											
					        )
			            )
		            )
	            )
            "; 
            #endregion

            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddEntity(typeof(User))
                //.AddScalar("Id", NHibernateUtil.Int32)
                //.AddScalar("Name", NHibernateUtil.String)
                .SetInt32("userId", userId)
                .SetInt32("roleId", (int)role);

            return query.List<User>();
            //return query.SetResultTransformer(Transformers.AliasToBean<IdNameDto>()).List<IdNameDto>();
        }

        public virtual IList<User> GetManualRoleHoldersForUser(User user, UserManualRole role)
        {
            return GetManualRoleHoldersForUser(user.Id, role);
        }
    }
}