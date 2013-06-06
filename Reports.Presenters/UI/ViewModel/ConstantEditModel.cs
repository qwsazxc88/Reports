using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class ConstantEditModel
    {
        public int Id { get; set; }
        public int TS { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }

        [Display(Name = "Месяц")]
        public int Month { get; set; }
        public IList<IdNameDto> Months { get; set; }

        [Display(Name = "Баланс мес.(дней)")]
        [Required(ErrorMessageResourceName = "ConstantEditModel_Days_Required", ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("ConstantEditModel_Days_Required", typeof(Resources))]
        public string Days { get; set; }

        [Display(Name = "Баланс мес.(часов)")]
        [Required(ErrorMessageResourceName = "ConstantEditModel_Hours_Required", ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("ConstantEditModel_Hours_Required", typeof(Resources))]
        public string Hours { get; set; }

        public bool ReloadPage { get; set; }

    }
}
