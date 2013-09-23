using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class DeductionUserInfoModel //: PreventDCModel
    {
        [Display(Name = "Структурное подразделение")]
        public string Department { get; set; }
        public string DateRelease { get; set; }
        public string UserInfoError { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "СНИЛС")]
        public string Cnilc { get; set; }
    }
}