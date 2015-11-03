﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class DocumentMovements: AbstractEntity
    {
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? SendDate { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public virtual User User { get; set; }
        public virtual string Descript { get; set; }
        public virtual DocumentMovements_SelectedDocs Docs { get; set; }
    }
}
