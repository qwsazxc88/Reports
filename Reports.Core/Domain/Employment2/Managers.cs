namespace Reports.Core.Domain
{
    public class Managers : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual Position Position { get; set; } // OK
        public virtual Department Directorate { get; set; } // OK?
        public virtual Department Department { get; set; } // OK
        public virtual string EmploymentConditions { get; set; } // OK
        public virtual string Schedule { get; set; } // OK?
        public virtual string ProbationaryPeriod { get; set; } // OK?
        public virtual decimal? SalaryBasis { get; set; }
        public virtual string WorkCity { get; set; } // OK
        public virtual decimal? PersonalAddition { get; set; } // OK
        public virtual decimal? PositionAddition { get; set; } // OK
        public virtual bool? IsFront { get; set; } // OK
        public virtual decimal? Bonus { get; set; } // OK
        public virtual bool? IsLiable { get; set; } // OK
        public virtual string RequestNumber { get; set; } // OK
        // ? public virtual IList<EploymentApproval> EmploymentApprovals ?
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