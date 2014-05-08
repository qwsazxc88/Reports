using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class Automobile : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string Make { get; set; }
        public virtual string LicensePlateNumber { get; set; }
    }
}
