using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RequestNextNumberDao : DefaultDao<RequestNextNumber>, IRequestNextNumberDao
    {
        protected static object syncObject = new object();
        public RequestNextNumberDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public int GetNextNumberForType(int typeId)
        {
            lock(syncObject)
            {
                RequestNextNumber number = (RequestNextNumber)Session.CreateCriteria(typeof(RequestNextNumber))
                  .Add(Restrictions.Eq("RequestTypeId", typeId))
                  .UniqueResult();
                if(number == null)
                {
                    RequestNextNumber newNumber = new RequestNextNumber { RequestTypeId = typeId,NextNumber = 2};
                    SaveAndFlush(newNumber);
                    return newNumber.NextNumber - 1;
                }
                int returnValue = number.NextNumber++;
                SaveAndFlush(number);
                return returnValue;
            }
        }
    }
}