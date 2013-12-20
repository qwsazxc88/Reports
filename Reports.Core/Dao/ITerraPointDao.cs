using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface ITerraPointDao : IDao<TerraPoint>
    {
        IList<TerraPoint> GetTerraPointTree(int tpId);
        IList<TerraPoint> FindByLevelAndParentId(int level, string parentId);
        TerraPoint FindByCode1C(string code1C);
        IdNameDto FindByCode1CAndPath(string code1C, string path);
    }
}