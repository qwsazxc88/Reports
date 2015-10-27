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
    /// Справочник типов подразделений.
    /// </summary>
    public class StaffDepartmentTypesDao : DefaultDao<StaffDepartmentTypes>, IStaffDepartmentTypesDao
    {
        public StaffDepartmentTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Список типов подразделений.
        /// </summary>
        /// <returns></returns>
        public IList<IdNameDto> GetDepartmentTypes()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id, Name FROM StaffDepartmentTypes")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String);
                
            return query.SetResultTransformer(Transformers.AliasToBean<IdNameDto>()).List<IdNameDto>();
        }
    }
}
