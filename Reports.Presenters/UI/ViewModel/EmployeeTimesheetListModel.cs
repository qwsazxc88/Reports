using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class EmployeeTimesheetListModel
    {
        public int OwnerId { get; set; }
        public IList<DocumentDtoModel> Timesheets { get; set; }
    }
}