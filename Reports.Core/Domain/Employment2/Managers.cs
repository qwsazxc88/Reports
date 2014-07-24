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
        public virtual string Schedule { get; set; } // OK?
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

        public bool? ManagerApprovalStatus { get; set; }
        public User ApprovingManager { get; set; }
        public string ManagerRejectionReason { get; set; }

        public bool? HigherManagerApprovalStatus { get; set; }
        public User ApprovingHigherManager { get; set; }
        public string HigherManagerRejectionReason { get; set; }

        public User RejectingChief { get; set; }
        public string ChiefRejectionReason { get; set; }

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