using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RequestPrintFormDao : DefaultDao<RequestPrintForm>, IRequestPrintFormDao
    {
        public RequestPrintFormDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual RequestPrintForm FindByRequestAndTypeId(int requestId, RequestPrintFormTypeEnum typeId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(RequestPrintForm));
            criteria.Add(Restrictions.Eq("RequestId", requestId))
                    .Add(Restrictions.Eq("RequestTypeId",(int)typeId));
            return (RequestPrintForm)criteria.UniqueResult();
        }
    }
}