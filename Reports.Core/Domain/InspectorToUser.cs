namespace Reports.Core.Domain
{
    public class InspectorToUser : AbstractEntity //AbstractUsedEntity
    {
        #region Constants

        #endregion

        #region Fields

        #endregion

        #region Properties

        public virtual User Inspector { get; set; }
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