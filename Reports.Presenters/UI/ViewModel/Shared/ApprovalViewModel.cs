using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel
{
    public class ApprovalViewModel
    {
        public string Name { get; set; }
        public DateTime? CheckDate { get; set; }
        public bool IsChecked {get;set;}
        public bool IsEditable { get; set; }       
    }
}
