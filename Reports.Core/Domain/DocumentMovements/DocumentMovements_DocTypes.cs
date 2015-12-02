using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class DocumentMovements_DocTypes: AbstractEntity
    {
        public virtual string Name {get;set;}
	    public virtual bool FromBank {get;set;}
        public virtual bool ToBank { get; set; }
    }
}
