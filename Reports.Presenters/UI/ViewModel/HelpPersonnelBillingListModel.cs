using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpPersonnelBillingListModel : BeginEndCreateDate
    {
      
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        //public bool DepartmentReadOnly { get; set; }

        [Display(Name = "Статус запроса")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        [Display(Name = "ФИО отправителя")]
        public string InitiatorUserName { get; set; }

        [Display(Name = "ФИО получателя")]
        public string WorkerUserName { get; set; }

        [Display(Name = "Номер запроса")]
        public string Number { get; set; }

        [Display(Name = "Тема запроса")]
        public int TitleId { get; set; }
        public IList<IdNameDto> Titles;
        //public string Title { get; set; }

        [Display(Name = "Срочность")]
        public int UrgencyId { get; set; }
        public IList<IdNameDto> Urgencies;


        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public bool IsAddAvailable { get; set; }
        
        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public IList<HelpPersonnelBillingRequestDto> Documents { get; set; }

        public bool HasErrors { get; set; }
    }
}
