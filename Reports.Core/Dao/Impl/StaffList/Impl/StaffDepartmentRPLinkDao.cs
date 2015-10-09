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
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id, A.Code, A.Name, A.BGId, B.Name as BGName, A.DepartmentId, C.Name as DepName
                                                    FROM StaffDepartmentRPLink as A
                                                    LEFT JOIN StaffDepartmentBusinessGroup as B ON B.Id = A.BGId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("BGId", NHibernateUtil.Int32)
                .AddScalar("BGName", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentRPLinkDto>()).List<StaffDepartmentRPLinkDto>();
        }
    }
}
