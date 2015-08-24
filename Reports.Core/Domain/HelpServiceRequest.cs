using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Заявка на услугу
    /// </summary>
    public class HelpServiceRequest : AbstractEntityWithVersion
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual DateTime? SendDate { get; set; }
        public virtual DateTime? BeginWorkDate { get; set; }
        public virtual DateTime? EndWorkDate { get; set; }
        public virtual DateTime? ConfirmWorkDate { get; set; }
        public virtual DateTime? RejectWorkDate { get; set; }
        public virtual DateTime? NotEndWorkDate { get; set; }
        public virtual Department FiredUserDepartment { get; set; }
        public virtual int Number { get; set; }

        public virtual HelpServiceType Type { get; set; }
        public virtual HelpServiceProductionTime ProductionTime { get; set; }
        public virtual HelpServiceTransferMethod TransferMethod { get; set; }
        public virtual HelpServicePeriod Period { get; set; }
        public virtual string Requirements { get; set; }
       

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }
        public virtual User Consultant { get; set; }

        public virtual string Address { get; set; }

        public virtual IList<HelpServiceRequestComment> Comments { get; set; }

        public virtual string FiredUserName { get; set; }
        public virtual string FiredUserSurname { get; set; }
        public virtual string FiredUserPatronymic { get; set; }
        public virtual DateTime? UserBirthDate { get; set; }
        public virtual NoteType Note { get; set; }
        public virtual bool IsOriginalReceived { get; set; }
        public virtual bool IsForGEMoney { get; set; }
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}