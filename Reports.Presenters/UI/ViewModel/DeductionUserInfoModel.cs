using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class DeductionUserInfoModel : PreventDCModel
    {
        [Display(Name = "Номер служебной записки")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Дата создания служебной записки")]
        public string DateEdited { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string Department { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "СНИЛС")]
        public string Cnilc { get; set; }
    }
}