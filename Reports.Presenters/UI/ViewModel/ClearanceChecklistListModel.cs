using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class ClearanceChecklistListModel : BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        public IList<ClearanceChecklistDto> Documents { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public ClearanceChecklistListModel()
        {
            this.Positions = new List<IdNameDto>();
            this.Statuses = new List<IdNameDto>();
            this.Documents = new List<ClearanceChecklistDto>();
        }
    }
}
