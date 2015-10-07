using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace Reports.Presenters.UI.ViewModel
{    
    public class FieldViewModel<T>
    {        
        public virtual T Value { get; set; }
        public bool IsEditable { get; set; }
        public string EditorClass { get; set; }
    }
}
