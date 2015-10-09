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
    /// Справочник бизнес-групп
    /// </summary>
    public class StaffDepartmentBusinessGroupDao : DefaultDao<StaffDepartmentBusinessGroup>, IStaffDepartmentBusinessGroupDao
    {
        public StaffDepartmentBusinessGroupDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// </summary>
        /// Бизнс-группы.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentBusinessGroupDto> GetDepartmentBusinessGroups()
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id, A.Code, A.Name, A.AdminId, B.Name as AdminName, A.DepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentBusinessGroup as A
                                                    LEFT JOIN StaffDepartmentAdministration as B ON B.Id = A.AdminId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("AdminId", NHibernateUtil.Int32)
                .AddScalar("AdminName", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentBusinessGroupDto>()).List<StaffDepartmentBusinessGroupDto>();
        }
    }
}
