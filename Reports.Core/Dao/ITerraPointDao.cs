using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface ITerraPointDao : IDao<TerraPoint>
    {
        IList<TerraPoint> GetDepartmentsTree(int tpId);
    }
}