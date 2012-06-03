using System;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AutoImportModel : IExportImport
    {
        [Display(Name = "Результат")]
        public bool Result { get; set; }
        [Display(Name = "Месяц")]
        public string ExportMonth { get; set; }
        public DateTime Month { get; set; }
        [Display(Name = "Ошибка")]
        public string Error { get; set; }
        public EmailDto EmailDto { get; set; }
    }
}