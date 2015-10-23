using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class MailConfirm : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string Mail { get; set; }
        public virtual User User { get; set; }        
    }
}
