using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class ExperienceItem : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual DateTime BeginningDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Company { get; set; }
        public virtual string Position { get; set; }
        public virtual string CompanyContacts { get; set; }
    }
}
