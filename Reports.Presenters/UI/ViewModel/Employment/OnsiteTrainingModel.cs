using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class OnsiteTrainingModel : AbstractEmploymentModel
    {
        [Display(Name = "Вид обучения"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string Type { get; set; } //ok

        [Display(Name = "Описание обучения"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string Description { get; set; } //ok

        [Display(Name = "Дата начала обучения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginningDate { get; set; } //ok

        [Display(Name = "Дата окончания обучения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; } //ok

        [Display(Name = "Обучение пройдено")]
        public bool? IsComplete { get; set; } // возможно, заменить отображение выпадающим списком

        [Display(Name = "Причины, по которым обучение не пройдено"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string ReasonsForIncompleteTraining { get; set; } //ok

        [Display(Name = "Результаты обучения"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string Results { get; set; } //ok
        
        [Display(Name = "Комментарии"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public string Comments { get; set; } //ok

        public bool IsApproveByTrainerAvailable { get; set; }
        
        public string ApproverName { get; set; }

        //состояние кандидата
        public IList<CandidateStateDto> CandidateState { get; set; }

        public OnsiteTrainingModel()
        {
            this.Version = 0;
        }
    }
}
