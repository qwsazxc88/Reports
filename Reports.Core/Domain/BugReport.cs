using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class BugReport: AbstractEntity
    {
       public User User {get;set;}
       public int UserRole {get;set;}
       public string Browser {get;set;}
       public string BrowserVersion {get;set;}
       public string Summary {get;set;}
       public string Description {get;set;}
       public string Guid {get;set;}
    }
}
