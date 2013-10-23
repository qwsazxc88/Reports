using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface ITerraGraphicDao : IDao<TerraGraphic>
    {
        IList<TerraGraphic> LoadForIdsList(List<int> userIds,int month, int year);
        IList<TerraGraphicDbDto> LoadDtoForIdsList(List<int> userIds,int month, int year);
    }
}