using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class UserInfoModel:PreventDCModel
    {
        [Display(Name = "Дата составления")]
        public string DateCreated { get; set; }

        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Номер документа")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Сотрудник")]
        public string UserName { get; set; }

        [Display(Name = "Табельный номер")]
        public string UserNumber { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string Department { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Руководитель")]
        public string ManagerName { get; set; }

        [Display(Name = "Кадровик(и)")]
        public string PersonnelName { get; set; }

        public string Prefix = "К";
        public string DocPrefix = "КУ";
    }
    public class PreventDCModel : IPreventDCModel
    {
        public string Guid { get; set; }
    }
    public interface IPreventDCModel
    {
        string Guid { get; set; }
    }
}