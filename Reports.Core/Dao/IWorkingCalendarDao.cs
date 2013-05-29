using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IWorkingCalendarDao : IDao<WorkingCalendar>
    {
        IList<WorkingCalendar> GetEntitiesBetweenDates(int month, int year);
    }
}