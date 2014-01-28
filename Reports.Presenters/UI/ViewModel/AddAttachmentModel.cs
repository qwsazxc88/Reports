using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class AddAttachmentModel
    {
        public int DocumentId { get; set; }

        //[Required(ErrorMessageResourceName = "AddCommentModel_Comment_Required",
        //    ErrorMessageResourceType = typeof(Resources))]
        //[LocalizationDisplayName("AddCommentModel_Comment_Required", typeof(Resources))]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public string Error { get; set; }
        public bool IsDescriptionDisabled { get; set; }
    }
}