using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class MailList: AbstractEntity
    {
        public virtual DateTime? SendDate
        {
            get;
            set;
        }
        public virtual User To { get; set; }
        public virtual string Text {get;set;}
        public virtual string Subject { get; set; }
    }
}
