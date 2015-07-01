using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Реквизиты инициатора.
    /// </summary>
    public class DepRequestInfoModel
    {
        [Display(Name = "Дата заявки")]
        public DateTime? DateRequest { get; set; }

        [Display(Name = "Номер заявки")]
        public int? Id { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        [Display(Name = "Инициатор")]
        public string RequestInitiator { get; set; } //фио + должность

        [Display(Name = "Руководитель 5 уровня")]
        public string DepManager5 { get; set; }

        [Display(Name = "Руководитель 4 уровня")]
        public string DepManager4 { get; set; }

        [Display(Name = "Руководитель 3 уровня")]
        public string DepManager3 { get; set; }

        [Display(Name = "Руководство банка")]
        public string DepManager2 { get; set; }
    }
}
