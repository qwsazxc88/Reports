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
    /// Справочник принадлежностей подразделения.
    /// </summary>
    public class StaffDepartmentAccessoryDao : DefaultDao<StaffDepartmentAccessory>, IStaffDepartmentAccessoryDao
    {
        public StaffDepartmentAccessoryDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список принадлежностей.
        /// </summary>
        /// <returns></returns>
        public IList<IdNameDto> GetAccessoryes()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id, Name FROM StaffDepartmentAccessory")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<IdNameDto>()).List<IdNameDto>();
        }
    }
}
