using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AbsenceTypeDao : DefaultDao<AbsenceType>, IAbsenceTypeDao
    {
        public AbsenceTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IAbsenceTypeDao Members

        public IList<AbsenceType> LoadAllSorted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof (AbsenceType));
            criteria.AddOrder(new Order("Name", true));
            return criteria.List<AbsenceType>();
        }

        #endregion
    }
}