using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpUserInfoModel : PreventDCModel
    {
        [Display(Name = "Дата")]
        public string DateCreated { get; set; }

        [Display(Name = "Номер заявки")]
        public string DocumentNumber { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Должность сотрудника")]
        public string Position { get; set; }

        [Display(Name = "Дирекция")]
        public string Department1 { get; set; }

        [Display(Name = "Подразделение")]
        public string Department2 { get; set; }

        [Display(Name = "Руководитель")]
        public string ManagerName { get; set; }

        [Display(Name = "Заявку создал(а)")]
        public string Creator { get; set; }

        [Display(Name = "Исполнитель")]
        public string Worker { get; set; }

        [Display(Name = "Услуга сформирована")]
        public string WorkerEndDate { get; set; }

        [Display(Name = "Услуга оказана")]
        public string ConfirmDate { get; set; }
    }
}