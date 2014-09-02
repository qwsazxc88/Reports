using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class Education : AbstractEntityWithVersion, IEmploymentInfoSection
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual IList<HigherEducationDiploma> HigherEducationDiplomas { get; set; } //ok
        public virtual IList<PostGraduateEducationDiploma> PostGraduateEducationDiplomas { get; set; } //ok
        public virtual IList<Certification> Certifications { get; set; } //ok
        public virtual IList<Training> Training { get; set; } //ok

        public virtual bool IsFinal { get; set; }
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