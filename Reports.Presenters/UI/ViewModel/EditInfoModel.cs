using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class EditInfoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "EditInfoModel_Subject_Required",
            ErrorMessageResourceType = typeof (Resources))]
        [LocalizationDisplayName("EditInfoModel_Subject_Required", typeof(Resources))]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "EditInfoModel_Message_Required",
            ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("EditInfoModel_Message_Required", typeof(Resources))]
        [Display(Name = "Информация")]
        public string Message { get; set; }

        public string Error { get; set; }
    }
}