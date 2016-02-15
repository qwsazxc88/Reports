using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class BugReport: AbstractEntity
    {
       public virtual User User {get;set;}
       public virtual int UserRole {get;set;}
       public virtual string Browser {get;set;}
       public virtual string BrowserVersion {get;set;}
       public virtual string Summary {get;set;}
       public virtual string Description {get;set;}
       public virtual string Guid {get;set;}
    }
}
