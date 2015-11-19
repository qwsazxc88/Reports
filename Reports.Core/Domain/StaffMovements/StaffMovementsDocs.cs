using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class StaffMovementsDocs:AbstractEntity
    {        
	   public virtual StaffMovements Request {get;set;}
	   public virtual int DocType {get;set;}
	   public virtual bool IsRequired {get;set;}
       public virtual RequestAttachment Attachment { get; set; }
    }
}
