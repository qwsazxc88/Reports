using System;
using System.Text;
using System.Collections.Generic;


namespace Reports.Core.Domain {
    
    public class Menuforuser {
        public virtual int Id { get; set; }
        public virtual User Users { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual bool? Notallow { get; set; }
    }
}
