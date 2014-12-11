namespace Reports.Core.Domain
{
    public class HelpServiceType : AbstractEntity,ISortOrder//AbstractUsedEntity
    {
        #region Constants

        #endregion

        #region Fields

        #endregion

        #region Properties

        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int SortOrder { get; set; }

        public virtual bool IsPeriodAvailable { get; set; }
        public virtual bool IsRequirementsAvailable { get; set; }
        public virtual bool IsAttachmentAvailable { get; set; }

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