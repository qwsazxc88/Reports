using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class SurchargeNote : AbstractEntityWithVersion
    {
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? PersonnelDateAccept { get; set; }
        public virtual DateTime? CountantDateAccept { get; set; }
        public virtual DateTime PayDay { get; set; }
        public virtual int Number { get; set; }
        public virtual int NoteType { get; set; }
        public virtual User User { get; set; }
        public virtual User Creator { get; set; }
        public virtual User Personnel { get; set; }
        public virtual User Countant { get; set; }
        public virtual Department DocDep3 { get; set; }
        public virtual Department DocDep7 { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual User PersonnelManagerBank {get;set;}
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }
    }
}
