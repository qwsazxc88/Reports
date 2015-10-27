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
    /// Справочник причин внесения.
    /// </summary>
    public class StaffDepartmentReasonsDao : DefaultDao<StaffDepartmentReasons>, IStaffDepartmentReasonsDao
    {
        public StaffDepartmentReasonsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Список причин занесения в справочник подразделений.
        /// </summary>
        /// <returns></returns>
        public IList<IdNameDto> GetDepartmentReasons()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id, Name FROM StaffDepartmentReasons")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<IdNameDto>()).List<IdNameDto>();
        }
    }
}
