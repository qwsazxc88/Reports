using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Удержание
    /// </summary>
    public class Deduction : AbstractEntityWithVersion//AbstractEntity
    {
        #region Fields
        public virtual DateTime EditDate { get; set; }
        public virtual DateTime DeductionDate { get; set; }

        public virtual int Number { get; set; }
        public virtual DeductionType Type { get; set; }
        public virtual DeductionKind Kind { get; set; }
        public virtual decimal Sum { get; set; }
        public virtual DateTime? DismissalDate { get; set; }
        public virtual bool? IsFastDismissal { get; set; }
        public virtual DateTime? EmailSendToUserDate { get; set; }
        
        public virtual User User { get; set; }
        public virtual User Editor { get; set; }

        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual bool DeleteAfterSendTo1C { get; set; }
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}