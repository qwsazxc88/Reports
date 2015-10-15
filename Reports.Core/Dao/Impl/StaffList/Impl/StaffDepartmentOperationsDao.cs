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
    /// Справочник операций подразделений.
    /// </summary>
    public class StaffDepartmentOperationsDao : DefaultDao<StaffDepartmentOperations>, IStaffDepartmentOperationsDao
    {
        public StaffDepartmentOperationsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список операций.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentOperationsDto> GetOperations()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id as oId, Name as oName, IsUsed as oIsUsed FROM StaffDepartmentOperations")
                .AddScalar("oId", NHibernateUtil.Int32)
                .AddScalar("oName", NHibernateUtil.String)
                .AddScalar("oIsUsed", NHibernateUtil.Boolean);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentOperationsDto>()).List<StaffDepartmentOperationsDto>();
        }
    }
}
