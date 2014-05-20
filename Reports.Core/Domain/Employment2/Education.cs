using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class Education : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual IList<HigherEducationDiploma> HigherEducationDiplomas { get; set; } // OK
        public virtual IList<PostGraduateEducationDiploma> PostGraduateEducationDiplomas { get; set; } // OK
        public virtual IList<Certification> Certifications { get; set; } // OK
        public virtual IList<Training> Training { get; set; } // OK
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