namespace Reports.Core.Domain
{
    public class UserToDepartment : AbstractEntityWithVersion //AbstractUsedEntity
    {
        #region Constants

        #endregion

        #region Fields

        #endregion

        #region Properties

        public virtual User User { get; set; }
        public virtual Department Department { get; set; }

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