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
    /// Справочник СКБ/GE.
    /// </summary>
    public class StaffDepartmentSKB_GEDao : DefaultDao<StaffDepartmentSKB_GE>, IStaffDepartmentSKB_GEDao
    {
        public StaffDepartmentSKB_GEDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список СКБ/GE.
        /// </summary>
        /// <returns></returns>
        public IList<IdNameDto> GetSKB_GE()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id, Name FROM StaffDepartmentSKB_GE")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<IdNameDto>()).List<IdNameDto>();
        }
    }
}
