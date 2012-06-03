using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class EditTypeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "EditTypeModel_Name_Required",
            ErrorMessageResourceType = typeof (Resources))]
        [LocalizationDisplayName("EditTypeModel_Name_Required", typeof (Resources))]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public string Error { get; set; }
    }
}