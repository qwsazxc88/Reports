using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel
{
    public class SortableModel
    {
        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }
    }
}
