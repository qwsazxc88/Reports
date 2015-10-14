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
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as aId, A.Code as aCode, A.Name as aName, A.ManagementId, B.Name as ManagementName, A.DepartmentId as aDepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentAdministration as A
                                                    LEFT JOIN StaffDepartmentManagement as B ON B.Id = A.ManagementId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("aId", NHibernateUtil.Int32)
                .AddScalar("aCode", NHibernateUtil.String)
                .AddScalar("aName", NHibernateUtil.String)
                .AddScalar("ManagementId", NHibernateUtil.Int32)
                .AddScalar("ManagementName", NHibernateUtil.String)
                .AddScalar("aDepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentAdministrationDto>()).List<StaffDepartmentAdministrationDto>();
        }
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        public bool IsEnableDelete(int Id)
        {
            IQuery query = Session.CreateSQLQuery("SELECT cast(case when count(*) > 0 then 0 else 1 end as bit) IsExists FROM dbo.StaffDepartmentBusinessGroup WHERE AdminId = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }
    }
}
