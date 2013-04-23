using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AllRequestListModel : BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        [Display(Name = "Период с")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        //public IList<IdNameDto> Statuses;

        public IList<AllRequestDto> Documents { get; set; }

    }

    
}