using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DepartmentDao : DefaultDao<Department>, IDepartmentDao
    {
        //public const string NameFieldName = "Name";

        public DepartmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        //public IList<Department> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(Department));
        //    criteria.AddOrder(new Order(NameFieldName, true));
        //    return criteria.List<Department>();
        //}
    }
}