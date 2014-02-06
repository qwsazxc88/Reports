namespace Reports.Core.Domain
{
    /// <summary>
    /// Причина удержания
    /// </summary>
    public class DeductionKind : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string CalculationStyle { get; set; }
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