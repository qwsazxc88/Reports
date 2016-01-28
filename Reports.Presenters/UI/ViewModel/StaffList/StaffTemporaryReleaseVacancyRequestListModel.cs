using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Реестр заявок на создание временных вакансий 
    /// </summary>
    public class StaffTemporaryReleaseVacancyRequestListModel
    {
        [Display(Name = "Структурное подразделение")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string Surname { get; set; }

        [Display(Name = "Код заявки")]
        public int? Id { get; set; }

        [Display(Name = "Код штатной единицы")]
        public int? SEPId { get; set; }

        [Display(Name = "Виды отсутствий")]
        public int AbsencesTypeId { get; set; }
        public IList<IdNameDto> AbsencesTypes { get; set; }

        [Display(Name = "Период создания заявки с ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateEnd { get; set; }

        public IList<StaffTemporaryReleaseVacancyDto> TemporaryReleaseVacancyList { get; set; }

        //сортировка
        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }   
    }
}
