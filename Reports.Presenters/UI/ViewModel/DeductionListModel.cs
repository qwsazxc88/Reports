using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DeductionListModel:BeginEndCreateDate
    {
        public IList<IdNameDto> Types { get; set; }
        public IList<IdNameDto> RequestStatuses { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        //[AutoComplete("AutoComplete", "Departments", "searchText")]
        //public IdNameReadonlyDto Department { get; set; }
        /*public int DepartmentId { get; set; }
        public IList<IdNameDto> Departments;*/


        [Display(Name = "Тип")]
        public int TypeId { get; set; }

        /*[Display(Name = "Вид отпуска")]
        public int VacationTypeId { get; set; }
        public IList<IdNameDto> VacationTypes;*/

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int RequestStatusId { get; set; }

        //[Display(Name = "Документы")]
        public IList<DeductionDto> Documents { get; set; }
        public decimal Total { get; set; } 

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }
        [Display(Name="Номер заявки")]
        public string Number { get; set; }
    }
}
