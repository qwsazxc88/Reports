using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;


namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник групп операций
    /// </summary>
    public class StaffDepartmentOperationGroupsDao : DefaultDao<StaffDepartmentOperationGroups>, IStaffDepartmentOperationGroupsDao
    {
        public StaffDepartmentOperationGroupsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
