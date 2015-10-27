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
    /// Справочник видов наличия кассы.
    /// </summary>
    public class StaffDepartmentCashDeskAvailableDao : DefaultDao<StaffDepartmentCashDeskAvailable>, IStaffDepartmentCashDeskAvailableDao
    {
        public StaffDepartmentCashDeskAvailableDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список видов наличия кассы.
        /// </summary>
        /// <returns></returns>
        public IList<IdNameDto> GetCashDeskAvailable()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id, Name FROM StaffDepartmentCashDeskAvailable")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<IdNameDto>()).List<IdNameDto>();
        }
    }
}
