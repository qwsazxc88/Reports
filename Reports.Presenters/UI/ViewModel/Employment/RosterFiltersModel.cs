using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class RosterFiltersModel : SortableModel
    {
        // Фильтры
        public int DepartmentId { get; set; }
        public int? StatusId { get; set; }
        public string UserName { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EmploymentDateBegin { get; set; }
        public DateTime? EmploymentDateEnd { get; set; }
        public int? CandidateId { get; set; }
        public string ContractNumber1C { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string AppointmentReportNumber { get; set; }
        public int? AppointmentNumber { get; set; }
        public int PersonnelId { get; set; }
        public int? AdditionId { get; set; }
    }
}
