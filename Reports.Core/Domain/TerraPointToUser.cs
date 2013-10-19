namespace Reports.Core.Domain
{
    public class TerraPointToUser : AbstractEntity //AbstractUsedEntity
    {
        #region Properties

        public virtual TerraPoint TerraPointId { get; set; }
        public virtual int UserId { get; set; }

        #endregion
    }
}