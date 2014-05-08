using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class NameChange : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string PreviousName { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Place { get; set; }
        public virtual string Reason { get; set; }
    }
}