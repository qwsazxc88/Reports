using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class TrainingModel
    {
        [Display(Name = "Вид обучения"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string Type { get; set; }

        [Display(Name = "Описание обучения"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string Description { get; set; }

        [Display(Name = "Дата начала обучения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginningDate { get; set; }

        [Display(Name = "Дата окончания обучения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Обучение пройдено/не пройдено")]
        public bool IsComplete { get; set; }

        [Display(Name = "Причины, по которым обучение не пройдено"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string ReasonsForIncompleteTraining { get; set; }

        [Display(Name = "Результаты обучения"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string Results { get; set; }
    }
}
