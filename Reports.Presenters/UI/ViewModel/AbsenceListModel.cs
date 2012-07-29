using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AbsenceListModel
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public int DepartmentId { get; set; }
        public IList<IdNameDto> Departments;
        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;
        [Display(Name = "Вид невыхода")]
        public int AbsenceTypeId { get; set; }
        public IList<IdNameDto> AbsenceTypes;

        [Display(Name = "Дата начала")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "Дата окончания")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int RequestStatusId { get; set; }
        public IList<IdNameDto> RequestStatuses;

        //[Display(Name = "Документы")]
        public IList<VacationDto> Documents { get; set; }

    }

    
}