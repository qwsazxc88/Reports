using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Filters;

namespace Reports.Core.Dao
{
    public interface IUserLoginDao : IDao<UserLogin>
    {
        IList<UserLoginItemDto> FindByFilter(UserLoginFilter filter, out int count);
    }
}
