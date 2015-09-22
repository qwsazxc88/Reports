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
    /// Связи групп и установленного банковского ПО.
    /// </summary>
    public class StaffDepartmentSoftGroupLinksDao : DefaultDao<StaffDepartmentSoftGroupLinks>, IStaffDepartmentSoftGroupLinksDao
    {
        public StaffDepartmentSoftGroupLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Связи ПО с группами.
        /// </summary>
        /// <param name="GroupId">Id группы ПО.</param>
        /// <returns></returns>
        public IList<SoftGroupLinkDto> GetSoftGroupLinks(int GroupId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as SoftId, A.Name, B.Id, cast(case when B.Id is null then 0 else isnull(B.IsUsed, 0) end as bit) as IsUsed
                                                    FROM StaffDepartmentInstallSoft as A
                                                    LEFT JOIN StaffDepartmentSoftGroupLinks as B ON B.SoftId = A.Id and B.SoftGroupId = :GroupId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("SoftId", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("IsUsed", NHibernateUtil.Boolean)
                .SetInt32("GroupId", GroupId);
            return query.SetResultTransformer(Transformers.AliasToBean<SoftGroupLinkDto>()).List<SoftGroupLinkDto>();
        }
    }
}
