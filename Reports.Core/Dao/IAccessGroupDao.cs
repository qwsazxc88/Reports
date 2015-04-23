using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IAccessGroupDao : IDao<AccessGroup>
    {
        //IList<Position> LoadAllSorted();
        IList<AccessGroup> GetAccessGroups();
        IList<AccessGroupListDto> GetAccessGroupList(
                Department depFromFilter,
                string AccessGroupCode,
                string userName,
                int sortBy,
                bool? sortDescending);
    }
}