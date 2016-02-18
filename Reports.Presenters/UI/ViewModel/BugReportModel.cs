using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class BugReportModel
    {
        [Display(Name="Краткое описание")]
        public string Summary { get; set; }
        [Display(Name = "Подробное описание")]
        public string Description { get; set; }
        [Display(Name = "Версия браузера")]
        public string BrowserVersion { get; set; }
        [Display(Name = "Браузер")]
        public string Browser { get; set; }
    }
}
