using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MilitaryRankDao : DefaultDao<MilitaryRank>, IMilitaryRankDao
    {
        public const string NameFieldName = "Name";

        public MilitaryRankDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IMilitaryRankDao Members

        //public IList<Position> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof (Position));
        //    criteria.AddOrder(new Order(NameFieldName, true));
        //    return criteria.List<Position>();
        //}

        #endregion
    }
}