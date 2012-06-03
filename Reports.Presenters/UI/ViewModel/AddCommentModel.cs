using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class AddCommentModel
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessageResourceName = "AddCommentModel_Comment_Required",
            ErrorMessageResourceType = typeof (Resources))]
        [LocalizationDisplayName("AddCommentModel_Comment_Required", typeof (Resources))]
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        public string Error { get; set; }
    }
}