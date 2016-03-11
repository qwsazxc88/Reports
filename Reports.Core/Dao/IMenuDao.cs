using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    interface IMenuDao : IDao<Menu>
    {
        IList<MenuDto> GetMenuForRole(int userRole);
        bool AddMenuForRole(int userRole, int menuId);
        bool RemoveMenuForRole(int userRole, int menuId);
    }
}
