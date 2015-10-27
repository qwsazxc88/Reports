using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Налоговые реквизиты.
    /// </summary>
    public class StaffDepartmentTaxDetailsDao : DefaultDao<StaffDepartmentTaxDetails>, IStaffDepartmentTaxDetailsDao
    {
        public StaffDepartmentTaxDetailsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем налоговые реквизиты по идентификатору подразделения.
        /// </summary>
        /// <param name="Dep">Подразделение.</param>
        /// <returns></returns>
        public StaffDepartmentTaxDetails GetDetailsByDepartmentId(Department dep)
        {
            return (StaffDepartmentTaxDetails)Session.CreateCriteria(typeof(StaffDepartmentTaxDetails))
                   .Add(Restrictions.Eq("Department", dep)).UniqueResult();
        }
    }
}
