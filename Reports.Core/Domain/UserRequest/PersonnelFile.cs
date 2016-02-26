using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class PersonnelFile: AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual DocumentPlace Place { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public virtual DateTime? SendDate { get; set; }
        public virtual DateTime? ReceiveDate { get; set; }
        public virtual DateTime? CancelDate { get; set; }
        public virtual DateTime? ArchiveDate { get; set; }
        public virtual bool IsArchive { get; set; }

    }
}
