using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface ITerraGraphicDao : IDao<TerraGraphic>
    {
        IList<TerraGraphic> LoadForIdsList(List<int> userIds,int month, int year);
    }
}