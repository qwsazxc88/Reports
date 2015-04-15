using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Внутренний биллинг (запрос) 
    /// </summary>
    public class HelpPersonnelBillingRequest : AbstractEntityWithVersion
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual DateTime? SendDate { get; set; }
        public virtual DateTime? BeginWorkDate { get; set; }
        public virtual DateTime? EndWorkDate { get; set; }
        
        public virtual int Number { get; set; }

        public virtual HelpBillingTitle Title { get; set; }
        public virtual HelpBillingUrgency Urgency { get; set; }
        public virtual Department Department { get; set; }

        public virtual string UserName { get; set; }
        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }

        public virtual int RecipientId { get; set; }
        public virtual int RecipientRoleId { get; set; }
        public virtual User Creator { get; set; }
        public virtual int CreatorRoleId { get; set; }
        public virtual User Worker { get; set; }
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}