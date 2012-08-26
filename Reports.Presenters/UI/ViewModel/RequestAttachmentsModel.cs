using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class RequestAttachmentsModel
    {
        public int RequestId { get; set; }
        public int RequestTypeId { get; set; }
        public IList<RequestAttachmentModel> Attachments { get; set; }
    }

    public class RequestAttachmentModel
    {
        public int AttachmentId { get; set; }
        public string Attachment { get; set; }
        public string Description { get; set; }
    }
}