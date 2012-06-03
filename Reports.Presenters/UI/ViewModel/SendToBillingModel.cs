using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class SendToBillingModel : IEmailDtoSupport
    {
        public int DocumentId { get; set; }
        public string Error { get; set; }

        public EmailDto EmailDto { get; set; }
    }
}