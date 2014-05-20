using System;

namespace Reports.Core.Domain
{
    public class OnsiteTraining : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual OnsiteTrainingType Type { get; set; } // OK-
        public virtual string Description { get; set; } // OK
        public virtual DateTime? BeginningDate { get; set; } // OK
        public virtual DateTime? EndDate { get; set; } // OK
        public virtual bool? IsComplete { get; set; } // OK
        public virtual string ReasonsForIncompleteTraining { get; set; } // OK
        public virtual string Results { get; set; } // OK
        // ?
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