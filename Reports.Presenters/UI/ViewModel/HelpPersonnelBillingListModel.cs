using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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

        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        [Display(Name = "ФИО отправителя")]
        public string InitiatorUserName { get; set; }

        [Display(Name = "ФИО получателя")]
        public string WorkerUserName { get; set; }

        [Display(Name = "Номер заявки")]
        public string Number { get; set; }

        [Display(Name = "Тема заявки")]
        public string Title { get; set; }

        [Display(Name = "Срочность")]
        public string UrgencyId { get; set; }
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

        public bool HasErrors { get; set; }
    }
}
