using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PassportModel
    {
        public int Version { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Вид документа"),
            Required(ErrorMessage = "Обязательное поле")]
        public int DocumentType { get; set;}
        public IEnumerable<SelectListItem> DocumentTypeItems { get; set; }

        [Display(Name = "Серия", Prompt = "4 цифры"),
            RegularExpression(@"^\d{4}$", ErrorMessage = "Требуется 4 цифры"),
            Required(ErrorMessage = "Обязательное поле")]
        public string InternalPassportSeries { get; set; }

        [Display(Name = "Номер", Prompt = "6 цифр"),
            RegularExpression(@"^\d{6}$", ErrorMessage = "Требуется 6 цифр"),
            Required(ErrorMessage = "Обязательное поле")]
        public string InternalPassportNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime InternalPassportDateOfIssue { get; set; }

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
        public DateTime RegistrationDate { get; set; }

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

        [Display(Name = "Населенный пункт"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string City { get; set; }

        [Display(Name = "Улица"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Street { get; set; }
        [Display(Name = "Дом"),
            StringLength(6, ErrorMessage = "Не более 6 знаков.")]
        public string StreetNumber { get; set; }

        [Display(Name = "Корпус/строение"),
            StringLength(3, ErrorMessage = "Не более 3 знаков.")]
        public string Building { get; set; }

        [Display(Name = "Квартира"),
            StringLength(5, ErrorMessage = "Не более 5 знаков.")]
        public string Appartment { get; set; }

        // International Passport

        [Display(Name = "Серия"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string InternationalPassportSeries { get; set; }

        [Display(Name = "Номер"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string InternationalPassportNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InternationalPassportDateOfIssue { get; set; }

        [Display(Name = "Кем выдан"),
            StringLength(150, ErrorMessage = "Не более 150 знаков.")]
        public string InternationalPassportIssuedBy { get; set; }

        public PassportModel()
        {
            this.Version = 0;
        }
    }
}
