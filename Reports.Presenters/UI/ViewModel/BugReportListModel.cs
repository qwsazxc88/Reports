using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.ViewModel
{
    public class BugReportListModel
    {
        public IList<BugReportDto> Documents { get; set; }
    }
}
