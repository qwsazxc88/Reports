using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SearchModelAttribute:Attribute
    {
        public string RightsProperty { get; set; }
    }
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SearchFieldAttribute:Attribute
    {
        public string ModelParam { get; set; }
        public ComparerEnum Comparer { get; set; }
        public string Format { get; set; }
        public object IgnoreValue { get; set; }
        public bool IsNullable { get; set; }
    }
    public enum ComparerEnum
    {
        Equals = 1,
        LessThan = 2,
        GreaterThan =3,
        NotEquals = 4,
        EqualsOrLess =5,
        EqualsOrGreatar =6,
        Like =7,
        Department = 8,
    }
}
