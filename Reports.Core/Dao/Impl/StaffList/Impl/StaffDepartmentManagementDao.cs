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
    /// справочник дирекций.
    /// </summary>
    public class StaffDepartmentManagementDao : DefaultDao<StaffDepartmentManagement>, IStaffDepartmentManagementDao
    {
        public StaffDepartmentManagementDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// </summary>
        /// Справочник дирекций.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentManagementDto> GetDepartmentManagements()
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as mId, A.Code as mCode, A.Name as mName, A.BranchId, B.Name as BranchName, A.DepartmentId as mDepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentManagement as A
                                                    LEFT JOIN StaffDepartmentBranch as B ON B.Id = A.BranchId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("mId", NHibernateUtil.Int32)
                .AddScalar("mCode", NHibernateUtil.String)
                .AddScalar("mName", NHibernateUtil.String)
                .AddScalar("BranchId", NHibernateUtil.Int32)
                .AddScalar("BranchName", NHibernateUtil.String)
                .AddScalar("mDepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentManagementDto>()).List<StaffDepartmentManagementDto>();
        }

        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        public bool IsEnableDelete(int Id)
        {
            IQuery query = Session.CreateSQLQuery("SELECT cast(case when count(*) > 0 then 0 else 1 end as bit) IsExists FROM dbo.StaffDepartmentAdministration WHERE ManagementId = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }
    }
}
