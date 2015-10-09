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
    /// Справочник управлений.
    /// </summary>
    public class StaffDepartmentAdministrationDao : DefaultDao<StaffDepartmentAdministration>, IStaffDepartmentAdministrationDao
    {
        public StaffDepartmentAdministrationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// </summary>
        /// Управления.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentAdministrationDto> GetDepartmentAdministrations()
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id, A.Code, A.Name, A.ManagementId, B.Name as ManagementName, A.DepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentAdministration as A
                                                    LEFT JOIN StaffDepartmentManagement as B ON B.Id = A.ManagementId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("ManagementId", NHibernateUtil.Int32)
                .AddScalar("ManagementName", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentAdministrationDto>()).List<StaffDepartmentAdministrationDto>();
        }
    }
}
