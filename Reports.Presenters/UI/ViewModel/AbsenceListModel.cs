using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public interface BeginEndCreateDate
    {
        DateTime? BeginDate { get; set; }
        DateTime? EndDate { get; set; }
    }
    public class AbsenceListModel : BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        //[AutoComplete("AutoComplete", "Departments", "searchText")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }
        //public int DepartmentId { get; set; }
        //public IList<IdNameDto> Departments;

        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;
        [Display(Name = "Вид невыхода")]
        public int AbsenceTypeId { get; set; }
        public IList<IdNameDto> AbsenceTypes;

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int RequestStatusId { get; set; }
        public IList<IdNameDto> RequestStatuses;

        //[Display(Name = "Документы")]
        public IList<VacationDto> Documents { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; } 

    }

    
}