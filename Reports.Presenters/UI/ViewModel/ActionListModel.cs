using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Enum;

namespace Reports.Presenters.UI.ViewModel
{
    public class ActionListModel : IExportImport
    {
        [Display(Name = "Месяц")]
        [Required(ErrorMessageResourceName = "ActionListModel_Month_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("ActionListModel_Month_Required", typeof(Resources))]
        public DateTime Month { get; set; }
        public IList<DateDto> Monthes;
        public IList<IdNameDto> Actions { get; set; }
        public ExportImportType Type { get; set; }
        public string Error { get; set; }

        public EmailDto EmailDto { get; set; }
    }
}