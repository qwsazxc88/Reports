using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class DocumentMovements: AbstractEntity
    {
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
