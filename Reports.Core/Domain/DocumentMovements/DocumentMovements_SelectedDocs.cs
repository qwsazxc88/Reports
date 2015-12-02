using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class DocumentMovements_SelectedDocs: AbstractEntity
    {
        public virtual DocumentMovements Movement {get;set;}
	    public virtual DocumentMovements_DocTypes DocType {get;set;}
	    public virtual bool SenderCheck  {get;set;}
	    public virtual DateTime? SenderCheckDate {get;set;}
	    public virtual bool RecieverCheck {get;set;}
        public virtual DateTime? RecieverCheckDate { get; set; }
    }
}
