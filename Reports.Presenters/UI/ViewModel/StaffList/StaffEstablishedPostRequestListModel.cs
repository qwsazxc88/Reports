using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Реестр заявок для штатных единиц.
    /// </summary>
    public class StaffEstablishedPostRequestListModel
    {
        [Display(Name = "Структурное подразделение")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses { get; set; }

        [Display(Name = "Номер заявки")]
        public int? Id { get; set; }

        [Display(Name = "Код штатной единицы")]
        public int? SEPId { get; set; }

        [Display(Name = "ФИО инициатора")]
        public string Creator { get; set; }

        [Display(Name = "Период создания заявки с ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Вид заявки")]
        public int RequestTypeId { get; set; }
        public IList<StaffEstablishedPostRequestTypes> RequestTypes { get; set; }

        public IList<EstablishedPostRequestDto> EPRequestList { get; set; }

        //сортировка
        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public string MessageStr { get; set; }
    }
}
