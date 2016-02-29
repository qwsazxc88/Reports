using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class PersonnelManagerPlace:AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual DocumentPlace Place { get; set; }

    }
}
