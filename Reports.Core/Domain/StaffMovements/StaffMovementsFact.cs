using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class StaffMovementsFact: AbstractEntity
    {
        public virtual DateTime SendTo1C { get; set; }
        public virtual Guid MovementGuid { get; set; }
        public virtual User User { get; set; }
        public virtual StaffEstablishedPostRequest StaffEstablishedPostRequest { get; set; }
        public virtual StaffMovements StaffMovements { get; set; }
        public virtual bool IsOk { get; set; } 
        public virtual int? MaterialLiabilityDoc {get;set;}
        public virtual int? RequirementsOrderDoc {get;set;}
        public virtual int? ServiceOrderDoc {get;set;}
        public virtual int? AgreementDoc {get;set;}
        public virtual int? AgreementAdditionalDoc {get;set;}
        public virtual int? OrderDoc {get;set;}
        public virtual bool IsDocumentsReceived { get; set; } 
    }
}
