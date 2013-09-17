using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IRoleDao : IDao<Role>
    {
        //IList<Role> LoadAllSorted();
        IList<IdNameDto> LoadRolesForList(List<UserRole> list);
    }
}