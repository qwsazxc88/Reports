using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class EditSubTypeModel
    {
        public int TypeId { get; set; }

        [Display(Name = "Тип заявки")]
        public string TypeName { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "EditTypeModel_Name_Required",
            ErrorMessageResourceType = typeof (Resources))]
        [LocalizationDisplayName("EditTypeModel_Name_Required", typeof (Resources))]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public string Error { get; set; }
    }
}