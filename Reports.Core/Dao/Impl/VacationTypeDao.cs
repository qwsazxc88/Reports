using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class VacationTypeDao : DefaultDao<VacationType>, IVacationTypeDao
    {
        public const string NameFieldName = "Name";

        public VacationTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IVacationTypeDao Members

        public IList<VacationType> LoadAllSorted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof (VacationType));
            criteria.AddOrder(new Order(NameFieldName, true));
            return criteria.List<VacationType>();
        }

        #endregion
    }
}