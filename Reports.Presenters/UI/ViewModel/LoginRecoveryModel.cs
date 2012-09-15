using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class LoginRecoveryModel : IEmailDtoSupport
    {
        [Required(ErrorMessageResourceName = "LoginRecoveryModel_Email_Required",
            ErrorMessageResourceType = typeof (Resources))]
        [Display(Name = "Введите адрес электронной почты")]
        public string Email { get; set; }

        public string Error { get; set; }
        //public string Success { get; set; }
        public bool IsRecoverySuccess { get; set; }

        public bool IsSupportFormVisible { get; set; }
        [Display(Name = "Тема")]
        public string Subject { get; set; }
        [Display(Name = "Текст сообщения")]
        public string Text { get; set; }

        public EmailDto EmailDto { get; set; }
    }
}