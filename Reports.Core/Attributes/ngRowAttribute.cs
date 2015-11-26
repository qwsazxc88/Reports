using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ngGridRowAttribute : Attribute
    {        
        public string field { get; set; }
        public string displayName { get; set; }
        public bool enableSorting { get; set; }
        public bool enableFiltering { get; set; }
        public bool enableCellEdit { get; set; }
        public string type { get; set; }
        public string cellTemplate { get; set; }
        public string cellEditTemplate { get; set; }
    }
    public class ngGridRow
    {
        public ngGridRow(ngGridRowAttribute x)
        {            
            field = x.field;
            displayName = x.displayName;
            enableSorting = x.enableSorting;
            enableFiltering = x.enableFiltering;
            enableCellEdit = x.enableCellEdit;
            type = x.type;
            cellTemplate = x.cellTemplate;
            cellEditTemplate = x.cellEditTemplate;
        }
        public int? width { get; set; }
        public string field { get; set; }
        public string displayName { get; set; }
        public bool enableSorting { get; set; }
        public bool enableFiltering { get; set; }
        public bool enableCellEdit { get; set; }
        public string type { get; set; }
        public string cellTemplate { get; set; }
        public string cellEditTemplate { get; set; }
    }
}
