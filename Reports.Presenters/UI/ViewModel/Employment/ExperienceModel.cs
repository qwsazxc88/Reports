using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ExperienceModel
    {
        public int Version { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Трудовая деятельность")]
        public IList<ExperienceItemDto> ExperienceItems { get; set; } // TODO: EMPL Кнопка добавления записей

        [Display(Name = "Серия"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string WorkBookSeries { get; set; } //ok

        [Display(Name = "Номер"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string WorkBookNumber { get; set; } //ok

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? WorkBookDateOfIssue { get; set; } //ok

        [Display(Name = "Серия"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string WorkBookSupplementSeries { get; set; } //ok

        [Display(Name = "Номер"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string WorkBookSupplementNumber { get; set; } //ok

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? WorkBookSupplementDateOfIssue { get; set; } //ok

        public ExperienceModel()
        {
            this.Version = 0;
            this.ExperienceItems = new List<ExperienceItemDto>();
        }
    }
}
