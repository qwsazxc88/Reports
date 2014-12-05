using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpServiceRequestsListModel : BeginEndCreateDate
    {
        public IList<IdNameDto> Statuses;
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }


        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Номер заявки")]
        public string Number { get; set; }

        //[Display(Name = "Режим просмотра изменений")]
        //public bool IsCorrectionsOnlyModeOn { get; set; }

        public bool IsAddAvailable { get; set; }
        //public bool IsApproveAvailable { get; set; }
        //public bool IsApproveClick { get; set; }
        //public bool IsCorrectionsOnlyModeAvailable { get; set; }

        public IList<HelpServiceRequestDto> Documents { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public bool HasErrors { get; set; }
        public string Address { get; set; }
    }
}