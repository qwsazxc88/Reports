using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class Family : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual FamilyMember Spouse { get; set; } // OK-
        public virtual FamilyMember Father { get; set; } // OK-
        public virtual FamilyMember Mother { get; set; } // OK-
        public virtual IList<FamilyMember> Children { get; set; } // OK-
        public virtual string Cohabitants { get; set; } // OK
        // Скан свидетельства о браке
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