using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Enum;

namespace Reports.Presenters.UI.ViewModel
{
    public class RequestAttachmentsModel
    {
        public int AttachmentRequestId { get; set; }
        public int AttachmentRequestTypeId { get; set; }
        public bool IsAddAvailable { get; set; }
        public IList<RequestAttachmentModel> Attachments { get; set; }
    }

    public class RequestAttachmentModel
    {
        public int AttachmentId { get; set; }
        public string Attachment { get; set; }
        public string Description { get; set; }
        public bool IsDeleteAvailable { get; set; }
    }
    public class SaveAttacmentModel
    {

        public int EntityId { get; set; }
        public RequestAttachmentTypeEnum EntityTypeId { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
        public UploadFileDto FileDto { get; set; }
    }
    public class DeleteAttacmentModel
    {
        public int Id { get; set; }
        public string Error { get; set; }
    }
}