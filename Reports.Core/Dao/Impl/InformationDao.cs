using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class InformationDao : DefaultDao<Information>, IInformationDao
    {
        public InformationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        //public IList<Information> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(Information));
        //    criteria.AddOrder(new Order("Subject", true));
        //    return criteria.List<Information>();
        //}
    }
}
