namespace Reports.Core.Domain
{
    public class DismissalType : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        //public virtual int? Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Reason { get; set; }
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