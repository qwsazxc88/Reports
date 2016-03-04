using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class Sicklist : AbstractEntityWithVersion//AbstractEntity
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime BeginDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int DaysCount { get; set; }
        public virtual string PirusNumber { get; set; }
        public virtual int Number { get; set; }
       
        
        public virtual SicklistType Type { get; set; }
        public virtual SicklistBabyMindingType BabyMindingType { get; set; }
        public virtual SicklistPaymentPercent PaymentPercent { get; set; }

        public virtual string SicklistNumber { get; set; }
        //public virtual SicklistPaymentType PaymentType { get; set; }

        
        public virtual DateTime? PaymentBeginDate { get; set; }
        public virtual int? ExperienceYears { get; set; }
        public virtual int? ExperienceMonthes { get; set; }
        public virtual SicklistPaymentRestrictType RestrictType { get; set; }
        //public virtual int? PaymentLimit { get; set; }
        public virtual DateTime? PaymentDecreaseDate { get; set; }
        public virtual bool IsPreviousPaymentCounted { get; set; }
        public virtual bool Is2010Calculate { get; set; }
        public virtual bool IsAddToFullPayment { get; set; }
        public virtual bool IsOriginalReceived { get; set; }
        public virtual bool IsOriginalSended { get; set; }
        public virtual bool IsOriginalFilled { get; set; }
        public virtual DateTime? OriginalFilledDate {get;set;}
        public virtual DateTime? OriginalReceivedDate { get; set; }
        public virtual DateTime? OriginalSendDate { get; set; }

      
        public virtual TimesheetStatus TimesheetStatus { get; set; }
       

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual User ApprovedByManager { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual User ApprovedByPersonnelManager { get; set; }
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }

        public virtual IList<SicklistComment> Comments { get; set; }

        public virtual bool DeleteAfterSendTo1C { get; set; }
        public virtual bool IsContinued { get; set; }
        
        
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}