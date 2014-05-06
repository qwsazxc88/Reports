using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class Reference : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Patronymic { get; set; }
        public virtual string WorksAt { get; set; }
        public virtual string Position { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Relation { get; set; }
    }
}
