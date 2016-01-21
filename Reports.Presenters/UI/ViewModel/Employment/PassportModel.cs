using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PassportModel : AbstractEmploymentModel
    {
        [Display(Name = "Вид документа"),
            Required(ErrorMessage = "Обязательное поле")]
        public int DocumentTypeId { get; set;}
        public IEnumerable<SelectListItem> DocumentTypeItems { get; set; }

        [Display(Name = "Серия", Prompt = "## ##"),
            RegularExpression(@"^\S{2}\s\S{2}$", ErrorMessage = "Требуется 4 знака в формате ## ##"),
            Required(ErrorMessage = "Обязательное поле")]
        public string InternalPassportSeries { get; set; }

        [Display(Name = "Номер", Prompt = "6 цифр"),
            RegularExpression(@"^\d{6}$", ErrorMessage = "Требуется 6 цифр"),
            Required(ErrorMessage = "Обязательное поле")]
        public string InternalPassportNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime? InternalPassportDateOfIssue { get; set; }

        [Display(Name = "Кем выдан"),
            StringLength(150, ErrorMessage = "Не более 150 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string InternalPassportIssuedBy { get; set; }

        [Display(Name = "Код подразделения", Prompt = "6 цифр"),
            RegularExpression(@"^\d{3}-\d{3}$", ErrorMessage = "Требуется формат ###-###"),
            Required(ErrorMessage = "Обязательное поле")]
        public string InternalPassportSubdivisionCode { get; set; }

        [Display(Name = "Дата регистрации"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "Почтовый индекс", Prompt = "6 цифр"),
            RegularExpression(@"^\d{6}$", ErrorMessage = "Требуется 6 цифр"),
            Required(ErrorMessage = "Обязательное поле")]
        public string ZipCode { get; set; }

        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Region { get; set; }

        [Display(Name = "Район"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string District { get; set; }

        [Display(Name = "Город/Населенный пункт"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string City { get; set; }

        [Display(Name = "Улица"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Street { get; set; }
        [Display(Name = "Дом"),
            StringLength(10, ErrorMessage = "Не более 10 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string StreetNumber { get; set; }

        [Display(Name = "Корпус/строение"),
            StringLength(3, ErrorMessage = "Не более 3 знаков.")]
        public string Building { get; set; }

        [Display(Name = "Квартира"),
            StringLength(5, ErrorMessage = "Не более 5 знаков."),
            /*Required(ErrorMessage = "Обязательное поле")*/]
        public string Apartment { get; set; }

        // International Passport

        [Display(Name = "Серия"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string InternationalPassportSeries { get; set; }

        [Display(Name = "Номер"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string InternationalPassportNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InternationalPassportDateOfIssue { get; set; }

        [Display(Name = "Кем выдан"),
            StringLength(150, ErrorMessage = "Не более 150 знаков.")]
        public string InternationalPassportIssuedBy { get; set; }

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok

        public bool IsPassportDraft { get; set; } //ok

        public HttpPostedFileBase InternalPassportScanFile { get; set; }

        public string InternalPassportScanAttachmentFilename { get; set; }
        public int InternalPassportScanAttachmentId { get; set; }

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public PassportModel()
        {
            this.Version = 0;
        }
    }
}
