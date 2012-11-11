using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public interface BeginEndCreateDate
    {
        DateTime? BeginDate { get; }
        DateTime? EndDate { get; }
    }
    public class AbsenceListModel : BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        [AutoComplete("AutoComplete", "Departments", "searchText")]
        public IdNameReadonlyDto Department { get; set; }
        //public int DepartmentId { get; set; }
        //public IList<IdNameDto> Departments;

        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;
        [Display(Name = "Вид невыхода")]
        public int AbsenceTypeId { get; set; }
        public IList<IdNameDto> AbsenceTypes;

        [Display(Name = "Дата создания заявки с")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int RequestStatusId { get; set; }
        public IList<IdNameDto> RequestStatuses;

        //[Display(Name = "Документы")]
        public IList<VacationDto> Documents { get; set; }

    }

    
}