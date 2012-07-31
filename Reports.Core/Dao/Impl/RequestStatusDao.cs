using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RequestStatusDao : DefaultDao<RequestStatus>, IRequestStatusDao
    {
        public const string NameFieldName = "Name";

        public RequestStatusDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        //public IList<RequestStatus> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof (RequestStatus));
        //    criteria.AddOrder(new Order(NameFieldName, true));
        //    return criteria.List<RequestStatus>();
        //}
    }
}