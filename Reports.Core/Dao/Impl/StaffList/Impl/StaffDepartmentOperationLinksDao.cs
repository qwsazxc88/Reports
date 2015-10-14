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
    /// Операции проводимые подразделением.
    /// </summary>
    public class StaffDepartmentOperationLinksDao : DefaultDao<StaffDepartmentOperationLinks>, IStaffDepartmentOperationLinksDao
    {
        public StaffDepartmentOperationLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
