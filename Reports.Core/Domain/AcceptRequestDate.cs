using System;

namespace Reports.Core.Domain
{
    public class AcceptRequestDate : AbstractEntity //AbstractUsedEntity
    {
        #region Constants

        #endregion

        #region Fields

        #endregion

        #region Properties

        public virtual DateTime DateAccept { get; set; }
        public virtual DateTime DateCreate { get; set; }
        public virtual User User { get; set; }

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