using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Вопрос/ответ
    /// </summary>
    public class HelpQuestionRequest : AbstractEntityWithVersion
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual DateTime? SendDate { get; set; }
        public virtual DateTime? BeginWorkDate { get; set; }
        public virtual DateTime? EndWorkDate { get; set; }
        public virtual DateTime? ConfirmWorkDate { get; set; }
        public virtual DateTime? RejectWorkDate { get; set; }
        
        public virtual int Number { get; set; }

        public virtual HelpQuestionType Type { get; set; }
        public virtual HelpQuestionSubtype Subtype { get; set; }
        //public virtual HelpServiceProductionTime ProductionTime { get; set; }
        //public virtual HelpServiceTransferMethod TransferMethod { get; set; }
        //public virtual HelpServicePeriod Period { get; set; }
        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }
       

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }
        public virtual int CreatorRoleId { get; set; }
        public virtual User ConsultantOutsourcing { get; set; }
        public virtual User ConsultantPersonnel { get; set; }
        public virtual User ConsultantAccountant { get; set; }
        public virtual User ConsultantOutsorsingManager { get; set; }
        //public virtual User PersonnelManager { get; set; }
        public virtual int? ConsultantRoleId { get; set; }


        public virtual IList<HelpQuestionHistoryEntity> HistoryEntities { get; set; }

      
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}