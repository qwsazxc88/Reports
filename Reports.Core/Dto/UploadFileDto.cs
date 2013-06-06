namespace Reports.Core.Dto
{
    public class UploadFileDto
    {
        public string FileName { get; set; }
        public string ContextType { get; set; }
        public byte[] Context { get; set; }
    }
    public class UploadFilesDto
    {
        public UploadFileDto attachment;
        public UploadFileDto penAttachment;
        public UploadFileDto innAttachment;
        public UploadFileDto ndflAttachment;
    }
}