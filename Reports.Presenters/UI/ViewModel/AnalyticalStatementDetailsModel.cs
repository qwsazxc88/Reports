using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.ViewModel
{
    public class AnalyticalStatementDetailsModel
    {
        [Display(Name="Дата")]
        public string Date { get; set; }
        [Display(Name="Организация")]
        public string Organization { get; set; }
        [Display(Name="Номер документа")]
        public int Number { get; set; }
        [Display(Name="Сотрудник")]
        public string Employee { get; set; }
        [Display(Name="Табельный номер")]
        public int StatementNumber { get; set; }
        [Display(Name="Структурное подразделение")]
        public string Department { get; set; }
        [Display(Name = "Должность")]
        public string Position { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public IList<AnalyticalStatementDetailsDto> Documents { get; set; }
    }
}
