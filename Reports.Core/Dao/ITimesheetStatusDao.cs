﻿using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface ITimesheetStatusDao : IDao<TimesheetStatus>
    {
        //IList<TimesheetStatus> LoadAllSorted();
        TimesheetStatus GetStatusForShortName(string shortName);
    }
}