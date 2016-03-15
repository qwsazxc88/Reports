using System;
using System.Text;
using System.Collections.Generic;


namespace Reports.Core.Domain {
    
    public class Menu {
        public Menu() {
			Menuforrole = new List<Menuforrole>();
			Menuforuser = new List<Menuforuser>();
        }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Parent { get; set; }
        public virtual bool Isvisible { get; set; }
        public virtual int? Sortorder { get; set; }
        public virtual string Link { get; set; }
        public virtual IList<Menuforrole> Menuforrole { get; set; }
        public virtual IList<Menuforuser> Menuforuser { get; set; }
    }
}
