using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IWorkingGraphicDao : IDao<WorkingGraphic>
    {
        IList<WorkingGraphic> LoadForIdsList(List<int> userIds,
                                             int month, int year);

        IList<WorkingGraphic> LoadForIdsList(List<int> userIds, DateTime beginDate, DateTime endDate);
    }
}