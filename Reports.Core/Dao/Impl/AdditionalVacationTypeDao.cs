using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AdditionalVacationTypeDao : DefaultDao<AdditionalVacationType>, IAdditionalVacationTypeDao
    {
        public const string NameFieldName = "Name";

        public AdditionalVacationTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IVacationTypeDao Members

        //public IList<VacationType> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof (VacationType));
        //    criteria.AddOrder(new Order(NameFieldName, true));
        //    return criteria.List<VacationType>();
        //}

        #endregion
    }
}