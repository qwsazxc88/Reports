namespace Reports.Core.Domain
{
    /// <summary>
    /// Attachment
    /// </summary>
    public class RequestPrintForm : AbstractEntity
    {
        #region Fields

        #endregion

        #region Properties

        public virtual byte[] Context { get; set; }
        public virtual int RequestId { get; set; }
        public virtual int RequestTypeId { get; set; }
        public virtual string FilePath { get; set; }

        #endregion

        #region Constructors

        #endregion
    }
}