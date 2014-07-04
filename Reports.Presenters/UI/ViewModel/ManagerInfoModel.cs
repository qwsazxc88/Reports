using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class ManagerInfoModel : PreventDCModel
    {
        [Display(Name = "Дата составления")]
        public string DateCreated { get; set; }
        //public string DateCreatedHidden { get; set; }

        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Номер документа")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Руководитель (заказчик)")]
        public string UserName { get; set; }

        //[Display(Name = "Табельный номер")]
        //public string UserNumber { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string Department { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Специалист по подбору персонала")]
        public string StaffName { get; set; }

        //[Display(Name = "Руководитель")]
        //public string ManagerName { get; set; }

        //[Display(Name = "Кадровик(и)")]
        //public string PersonnelName { get; set; }
    }
}