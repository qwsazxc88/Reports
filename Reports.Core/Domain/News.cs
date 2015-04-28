using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class News: AbstractEntity
    {
        public virtual string Header { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime PostDate { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual User Author { get; set; }

    }
}
