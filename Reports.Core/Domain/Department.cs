namespace Reports.Core.Domain
{
    /// <summary>
    /// Департамент/офис и т.п.
    /// </summary>
    public class Department : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Code1C { get; set; }
        public virtual int? ParentId { get; set; }
        //public virtual Department Parent { get; set; }
        public virtual string Path { get; set; }
        public virtual int? ItemLevel { get; set; }
        public virtual string CodeSKD { get; set; }
        public virtual int? Priority { get; set; }
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