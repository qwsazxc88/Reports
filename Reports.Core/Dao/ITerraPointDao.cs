﻿using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface ITerraPointDao : IDao<TerraPoint>
    {
        IList<TerraPoint> GetTerraPointTree(int tpId);
        IList<TerraPoint> FindByLevelAndParentId(int level, string parentId);
    }
}