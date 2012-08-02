using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Attachment
    /// </summary>
    public class RequestAttachment : AbstractEntityWithVersion //AbstractEntity
    {
        #region Fields

        #endregion

        #region Properties

        public virtual string FileName { get; set; }
        public virtual string ContextType { get; set; }
        public virtual byte[] Context { get; set; }
        public virtual int RequestId { get; set; }
        public virtual int RequestType { get; set; }
        public virtual DateTime DateCreated { get; set; }
        //public virtual Document Document { get; set; }

        #endregion

        #region Constructors

        #endregion
    }
}