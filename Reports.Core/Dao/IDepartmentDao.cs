using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IDepartmentDao : IDao<Department>
    {
        IList<Department> LoadAllSorted();
    }
}