using System;

namespace Reports.Core.Domain
{
    public class Managers : AbstractEntityWithVersion, IEmploymentInfoSection
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual Position Position { get; set; } //ok
        public virtual Department Department { get; set; } //ok
        public virtual string EmploymentConditions { get; set; } //ok
        public virtual bool IsSecondaryJob { get; set; }
        public virtual bool IsExternalPTWorker { get; set; }
        public virtual string ProbationaryPeriod { get; set; } //ok
        public virtual decimal? SalaryBasis { get; set; } // базовый должностной оклад
        public virtual decimal? SalaryMultiplier { get; set; } // ставка        
        public virtual string WorkCity { get; set; } //ok
        public virtual bool IsFront { get; set; } //ok
        public virtual decimal? Bonus { get; set; } //ok
        public virtual bool IsLiable { get; set; } //ok
        public virtual string RequestNumber { get; set; } //ok
        public virtual DateTime? RegistrationDate { get; set; }
        public virtual DateTime? PlanRegistrationDate { get; set; }

        public virtual bool? ManagerApprovalStatus { get; set; }
        public virtual User ApprovingManager { get; set; }
        public virtual DateTime? ManagerApprovalDate { get; set; }
        public virtual string ManagerRejectionReason { get; set; }

        public virtual bool? HigherManagerApprovalStatus { get; set; }
        public virtual User ApprovingHigherManager { get; set; }
        public virtual DateTime? HigherManagerApprovalDate { get; set; }
        public virtual string HigherManagerRejectionReason { get; set; }

        public virtual User CancelRejectUser { get; set; }
        public virtual DateTime? CancelRejectDate { get; set; }
        public virtual User CancelRejectHigherUser { get; set; }
        public virtual DateTime? CancelRejectHigherDate { get; set; }

        public virtual User RejectingChief { get; set; }
        public virtual string ChiefRejectionReason { get; set; }
        public virtual string MentorName { get; set; }
        public virtual string PyrusNumber { get; set; }

        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        #endregion
    }
}