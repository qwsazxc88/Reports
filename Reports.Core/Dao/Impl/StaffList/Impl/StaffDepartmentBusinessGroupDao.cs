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
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as bId, A.Code as bCode, A.Name as bName, A.AdminId, B.Name as AdminName, A.DepartmentId as bDepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentBusinessGroup as A
                                                    LEFT JOIN StaffDepartmentAdministration as B ON B.Id = A.AdminId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("bId", NHibernateUtil.Int32)
                .AddScalar("bCode", NHibernateUtil.String)
                .AddScalar("bName", NHibernateUtil.String)
                .AddScalar("AdminId", NHibernateUtil.Int32)
                .AddScalar("AdminName", NHibernateUtil.String)
                .AddScalar("bDepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentBusinessGroupDto>()).List<StaffDepartmentBusinessGroupDto>();
        }
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        public bool IsEnableDelete(int Id)
        {
            IQuery query = Session.CreateSQLQuery("SELECT cast(case when count(*) > 0 then 0 else 1 end as bit) IsExists FROM dbo.StaffDepartmentRPLink WHERE BGId = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }
    }
}
