using System;
using System.Text;
using System.Collections.Generic;


namespace Reports.Core.Domain {
    
    public class MenuForRole {
        public virtual int Id { get; set; }
        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual bool? Notallow { get; set; }
    }
}
