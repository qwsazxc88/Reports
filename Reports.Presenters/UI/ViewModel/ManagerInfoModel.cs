﻿using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Руководитель (инициатор)")]
        public string UserName { get; set; }
        public int ManagerId { get; set; }
        //[Display(Name = "Табельный номер")]
        //public string UserNumber { get; set; }

        [Display(Name = "Структурное подразделение инициатора")]
        public string Department { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Специалист по подбору персонала")]
        public string StaffName { get; set; }
        public bool ShowStaff { get; set; }
        [Display(Name = "Вышестоящие руководители")]
        public string Chiefs { get; set; }

        //[Display(Name = "Кадровик(и)")]
        //public string PersonnelName { get; set; }
    }
}