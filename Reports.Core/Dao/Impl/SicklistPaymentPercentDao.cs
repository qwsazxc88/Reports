using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistPaymentPercentDao : DefaultDao<SicklistPaymentPercent>, ISicklistPaymentPercentDao
    {
        public SicklistPaymentPercentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        //public IList<SicklistPaymentPercent> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(SicklistPaymentPercent));
        //    criteria.AddOrder(new Order("Id", true));
        //    return criteria.List<SicklistPaymentPercent>();
        //}
    }
}