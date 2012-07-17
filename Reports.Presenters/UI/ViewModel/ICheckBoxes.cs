using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel
{
    public interface ICheckBoxes
    {
         bool IsApprovedByUser { get; set; }
         bool IsApprovedByUserHidden { get; set; }
         bool IsApprovedByUserEnable { get; set; }
         bool IsApprovedByManager { get; set; }
         bool IsApprovedByManagerHidden { get; set; }
         bool IsApprovedByManagerEnable { get; set; }
         bool IsApprovedByPersonnelManager { get; set; }
         bool IsApprovedByPersonnelManagerHidden { get; set; }
         bool IsApprovedByPersonnelManagerEnable { get; set; }
         bool IsPostedTo1C { get; set; }
         bool IsPostedTo1CHidden { get; set; }
         bool IsPostedTo1CEnable { get; set; }
    }
}
