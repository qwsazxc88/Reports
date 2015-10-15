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
        /// <summary>
        /// Список групп операций.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentOperationGroupsDto> GetOperationGroups()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id as gId, Name as gName, IsUsed as gIsUsed FROM StaffDepartmentOperationGroups")
                .AddScalar("gId", NHibernateUtil.Int32)
                .AddScalar("gName", NHibernateUtil.String)
                .AddScalar("gIsUsed", NHibernateUtil.Boolean);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentOperationGroupsDto>()).List<StaffDepartmentOperationGroupsDto>();
        }
    }
}
