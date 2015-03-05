using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class Family : AbstractEntityWithVersion, IEmploymentInfoSection
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual IList<FamilyMember> FamilyMembers { get; set; } // OK-
        public virtual string Cohabitants { get; set; } // OK
        // Скан свидетельства о браке

        public virtual bool IsFinal { get; set; }
        public virtual bool IsValidate { get; set; }
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