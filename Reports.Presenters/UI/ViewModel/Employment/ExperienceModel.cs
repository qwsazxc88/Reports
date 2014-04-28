using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class ExperienceModel
    {
        [Display(Name = "Трудовая деятельность")]
        public IList<ExperienceItemDto> ExperienceItems { get; set; }

        [Display(Name = "Общий стаж - лет"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string ExperienceYears { get; set; }

        [Display(Name = "месяцев"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string ExperienceMonths { get; set; }

        [Display(Name = "Серия"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string WorkBookSeries { get; set; }

        [Display(Name = "Номер"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string WorkBookNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WorkBookDateOfIssue { get; set; }

        [Display(Name = "Серия"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string WorkBookSupplementSeries { get; set; }

        [Display(Name = "Номер"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string WorkBookSupplementNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WorkBookSupplementDateOfIssue { get; set; }

        public ExperienceModel()
        {
            this.ExperienceItems = new List<ExperienceItemDto>();
        }
    }
}
