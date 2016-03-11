using System;
using System.Text;
using System.Collections.Generic;


namespace Reports.Core.Domain {
    
    public class Menu {
        public Menu() {
            Menuforrole = new List<MenuForRole>();
            Menuforuser = new List<MenuForUser>();
        }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Parent { get; set; }
        public virtual bool Isvisible { get; set; }
        public virtual int? Sortorder { get; set; }
        public virtual string LinkController { get; set; }
        public virtual string LinkAction { get; set; }
        public virtual IList<MenuForRole> Menuforrole { get; set; }
        public virtual IList<MenuForUser> Menuforuser { get; set; }
    }
}
