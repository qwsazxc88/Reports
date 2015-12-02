namespace Reports.Core.Domain
{
    public class Position : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// Признак удаленной должности.
        /// </summary>
        public virtual bool IsDeleted { get; set; }
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