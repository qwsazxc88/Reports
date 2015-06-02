using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class HelpPersonnelBillingComments : AbstractEntity
    {
        public virtual int UserId { get; set; }
        public virtual int HelpBillingId { get; set; }
        public virtual bool IsQuestion { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string Comment { get; set; }
    }
}
