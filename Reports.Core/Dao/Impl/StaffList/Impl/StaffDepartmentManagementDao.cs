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
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id, A.Code, A.Name, A.BranchId, B.Name as BranchName, A.DepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentManagement as A
                                                    LEFT JOIN StaffDepartmentBranch as B ON B.Id = A.BranchId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("BranchId", NHibernateUtil.Int32)
                .AddScalar("BranchName", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentManagementDto>()).List<StaffDepartmentManagementDto>();
        }
    }
}
