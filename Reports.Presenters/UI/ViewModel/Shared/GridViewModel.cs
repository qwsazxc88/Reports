using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel.Shared
{
    public class GridViewModel
    {
        public IList<GridColumn> Columns { get; set; }
        public string DataUrl { get; set; }

    }
    public class GridColumn
    {
        public GridColumnDisplayFormat Format {get;set;}
        public string Name {get;set;}
        public string FieldName { get; set; }
        public string LinkFormat { get; set; }        
    }
    
    public enum GridColumnDisplayFormat
    {
        Currency = 1,
        Date = 2,
        String = 3
    }
}
