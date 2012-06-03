using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmployeeDocumentTypeDao : DefaultDao<EmployeeDocumentType>, IEmployeeDocumentTypeDao
    {
        protected const string EmployeeDocumentTypeAlias = "EmployeeDocumentType";
        public const string NameFieldName = "Name";

        public EmployeeDocumentTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual bool IsTypeWithNameAndOtherIdExists(string name, int id)
        {
            return (int)Session.CreateCriteria(typeof(EmployeeDocumentType))
                              .Add(Restrictions.Eq("Name", name))
                              .Add(Restrictions.Not(Restrictions.Eq("Id", id)))
                              .SetProjection(Projections.RowCount())
                              .UniqueResult() > 0;
        }

        public IList<EmployeeDocumentType> LoadAllSorted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(EmployeeDocumentType));
            criteria.AddOrder(new Order(NameFieldName, true));
            return criteria.List<EmployeeDocumentType>();
        }
        public IList<EmployeeDocumentSubType> LoadAllSubtype()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(EmployeeDocumentSubType));
//            criteria.AddOrder(new Order(NameFieldName, true));
            return criteria.List<EmployeeDocumentSubType>();
        }
        public IList<EmployeeDocumentSubType> LoadSubtypeForType(int typeId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(EmployeeDocumentSubType));
            criteria
                .Add(Restrictions.Eq("Type.Id", typeId))
                .AddOrder(new Order(NameFieldName, true));
            return criteria.List<EmployeeDocumentSubType>();
        }
        //public EmployeeDocumentSubType LoadSubtype(int subtypeId)
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(EmployeeDocumentSubType));
        //    criteria.Add(Restrictions.Eq("Id", subtypeId));
        //    return (EmployeeDocumentSubType)criteria.UniqueResult();
        //}

    }
}
