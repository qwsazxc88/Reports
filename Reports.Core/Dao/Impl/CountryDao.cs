using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class CountryDao : DefaultDao<Country>, ICountryDao
    {
        public CountryDao(ISessionManager sessionManager)
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