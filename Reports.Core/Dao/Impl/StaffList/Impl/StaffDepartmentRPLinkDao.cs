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
    /// справочник РП-привязок
    /// </summary>
    public class StaffDepartmentRPLinkDao : DefaultDao<StaffDepartmentRPLink>, IStaffDepartmentRPLinkDao
    {
        public StaffDepartmentRPLinkDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// </summary>
        /// РП-привязки.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentRPLinkDto> GetDepartmentRPLinks()
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as rId, A.Code as rCode, A.Name as rName, A.BGId, B.Name as BGName, A.DepartmentId as rDepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentRPLink as A
                                                    LEFT JOIN StaffDepartmentBusinessGroup as B ON B.Id = A.BGId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("rId", NHibernateUtil.Int32)
                .AddScalar("rCode", NHibernateUtil.String)
                .AddScalar("rName", NHibernateUtil.String)
                .AddScalar("BGId", NHibernateUtil.Int32)
                .AddScalar("BGName", NHibernateUtil.String)
                .AddScalar("rDepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentRPLinkDto>()).List<StaffDepartmentRPLinkDto>();
        }
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        public bool IsEnableDelete(int Id)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT cast(case when count(*) > 0 then 0 else 1 end as bit) IsExists FROM dbo.StaffDepartmentRPLink WHERE DepartmentId is not null and Id = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }
    }
}
