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
    /// Справочник групп банковского ПО.
    /// </summary>
    public class StaffDepartmentSoftGroupDao : DefaultDao<StaffDepartmentSoftGroup>, IStaffDepartmentSoftGroupDao
    {
        public StaffDepartmentSoftGroupDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список признаков арендованных помещений.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentSoftGroupDto> GetSoftGroups()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id as gId, Name as gName FROM StaffDepartmentSoftGroup")
                .AddScalar("gId", NHibernateUtil.Int32)
                .AddScalar("gName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentSoftGroupDto>()).List<StaffDepartmentSoftGroupDto>();
        }
    }
}
