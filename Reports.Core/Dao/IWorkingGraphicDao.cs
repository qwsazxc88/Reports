using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IWorkingGraphicDao : IDao<WorkingGraphic>
    {
        IList<WorkingGraphic> LoadForIdsList(List<int> userIds,
                                             int month, int year);
    }
}