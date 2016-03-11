using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Dto
{
    public class MenuDto
    {
        public string Name { get; set; }
        public string LinkController { get; set; }
        public string LinkAction { get; set; }
        public int Parent { get; set; }
        public virtual IList<MenuForRole> Menuforrole { get; set; }
        public virtual IList<MenuForUser> Menuforuser { get; set; }
    }
}
