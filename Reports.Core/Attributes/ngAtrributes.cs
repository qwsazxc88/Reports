using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ngGridAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public bool SortEnabled { get; set; }
        public bool FilterEnabled { get; set; }
        public bool IsEditable { get; set; }
        public ngType Type { get; set; }

    }
    public enum ngType
    {
        String =1,
        Boolean = 2,
        Date =3,
        Number =4,
        Object =5

    }
}
