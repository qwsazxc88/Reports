using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Web;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ExperienceModel : AbstractEmploymentModel
    {
        [Display(Name = "Трудовая деятельность")]
        public IList<ExperienceItemDto> ExperienceItems { get; set; }

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

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok


        public HttpPostedFileBase WorkBookScanFile { get; set; }
        public HttpPostedFileBase WorkBookSupplementScanFile { get; set; }
        
        public string WorkBookScanAttachmentFilename { get; set; }
        public int WorkBookScanAttachmentId { get; set; }
        public string WorkBookSupplementScanAttachmentFilename { get; set; }
        public int WorkBookSupplementScanAttachmentId { get; set; }
        public int RowID { get; set; }


        //для проверки заполнения модальной формы
        [Display(Name = "Начало работы"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime? BeginningDate { get; set; }

        [Display(Name = "Окончание работы"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Организация"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Company { get; set; }

        [Display(Name = "Должность"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Position { get; set; }

        [Display(Name = "Адрес организации, телефон отдела кадров, секретаря"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string CompanyContacts { get; set; }
        public bool IsExperienceItemNotValid { get; set; }//для проверок заполнения сведения об образовании


        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public ExperienceModel()
        {
            this.Version = 0;
            this.ExperienceItems = new List<ExperienceItemDto>();
        }
    }
}
