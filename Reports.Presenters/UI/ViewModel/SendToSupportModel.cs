using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class SendToSupportModel : IEmailDtoSupport
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Error { get; set; }

        public EmailDto EmailDto { get; set; }
    }
}