namespace Reports.Core.Domain
{
    public class Account : AbstractEntity //AbstractUsedEntity
    {
        #region Constants

        #endregion

        #region Fields

        #endregion

        #region Properties

        public virtual string Code { get; set; }
        public virtual string Number { get; set; }
        public virtual bool IsDebitAccount { get; set; }

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