using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class Training : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime BeginningDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsComplete { get; set; }
        public string ReasonsForIncompleteTraining { get; set; }
        public string Results { get; set; }
    }
}
