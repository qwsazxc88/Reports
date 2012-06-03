namespace Reports.Core.Dto
{
    public class UploadFileDto
    {
        public string FileName { get; set; }
        public string ContextType { get; set; }
        public byte[] Context { get; set; }
    }
}