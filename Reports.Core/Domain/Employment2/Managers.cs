namespace Reports.Core.Domain
{
    public class Managers : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual Position Position { get; set; } //ok
        public virtual Department Directorate { get; set; } //ok
        public virtual Department Department { get; set; } //ok
        public virtual string EmploymentConditions { get; set; } //ok
        public virtual Schedule Schedule { get; set; } // OK?
        public virtual string ProbationaryPeriod { get; set; } //ok
        public virtual decimal? DailySalaryBasis { get; set; }
        public virtual decimal? HourlySalaryBasis { get; set; }
        public virtual decimal? SalaryMultiplier { get; set; }
        // TODO: EMPL несколько окладов и ставка
        // public virtual decimal SalaryBasis { get; set; }
        public virtual string WorkCity { get; set; } //ok
        public virtual decimal? PersonalAddition { get; set; } //ok
        public virtual decimal? PositionAddition { get; set; } //ok
        public virtual bool IsFront { get; set; } //ok
        public virtual decimal? Bonus { get; set; } //ok
        public virtual bool IsLiable { get; set; } //ok
        public virtual string RequestNumber { get; set; } //ok

        public virtual bool? ManagerApprovalStatus { get; set; }
        public virtual User ApprovingManager { get; set; }
        public virtual string ManagerRejectionReason { get; set; }

        public virtual bool? HigherManagerApprovalStatus { get; set; }
        public virtual User ApprovingHigherManager { get; set; }
        public virtual string HigherManagerRejectionReason { get; set; }

        public virtual User RejectingChief { get; set; }
        public virtual string ChiefRejectionReason { get; set; }

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