using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class PostGraduateEducationDiploma : AbstractEntity
    {
        public virtual User User { get; set; }
    }
}
