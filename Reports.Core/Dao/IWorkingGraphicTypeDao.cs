using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IWorkingGraphicTypeDao : IDao<WorkingGraphicType>
    {
        IList<WorkingGraphicTypeDto> GetWorkingGraphicTypeDtoForUsers(IList<int> userIds);
    }
}