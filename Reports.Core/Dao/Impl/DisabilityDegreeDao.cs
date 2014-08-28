using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DisabilityDegreeDao : DefaultDao<DisabilityDegree>, IDisabilityDegreeDao
    {
        public const string NameFieldName = "Name";

        public DisabilityDegreeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IPositionDao Members

        //public IList<Position> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof (Position));
        //    criteria.AddOrder(new Order(NameFieldName, true));
        //    return criteria.List<Position>();
        //}

        #endregion
    }
}