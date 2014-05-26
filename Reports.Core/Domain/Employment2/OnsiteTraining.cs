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
        public virtual string Type { get; set; } // ok
        public virtual string Description { get; set; } //ok
        public virtual DateTime BeginningDate { get; set; } //ok
        public virtual DateTime EndDate { get; set; } //ok
        public virtual bool IsComplete { get; set; } //ok
        public virtual string ReasonsForIncompleteTraining { get; set; } //ok
        public virtual string Results { get; set; } //ok
        public virtual bool IsConfirmed { get; set; } //ok
        public virtual string Comments { get; set; } //ok
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