using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Реестр заявок на создание, изменение, удаление подразделений.
    /// </summary>
    public class StaffDepartmentRequestListModel
    {
        [Display(Name = "Структурное подразделение")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        //статусы заявок

        [Display(Name = "Номер заявки")]
        public int? Id { get; set; }

        [Display(Name = "ФИО инициатора")]
        public string Creator { get; set; }

        [Display(Name = "Период создания заявки с ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateEnd { get; set; }

        //сортировка
        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }
    }
}
