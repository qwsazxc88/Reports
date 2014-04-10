using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class PassportModel
    {
        [Display(Name = "Серия"),
            StringLength(4, ErrorMessage = "Должно быть 4 знака.")]
        public string InternalPassportSeries { get; set; }
        [Display(Name = "Номер"),
            StringLength(6, ErrorMessage = "Должно быть 6 знаков.")]
        public string InternalPassportNumber { get; set; }
        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InternalPassportDateOfIssue { get; set; }
        [Display(Name = "Кем выдан"),
            StringLength(50, ErrorMessage = "Должно быть 50 знаков.")]
        public string InternalPassportIssuedBy { get; set; }
        [Display(Name = "Код подразделения"),
            StringLength(6, ErrorMessage = "Должно быть 6 знаков.")]
        public string SubdivisionCode { get; set; }
        [Display(Name = "Дата регистрации"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Почтовый индекс"),
            StringLength(6, ErrorMessage = "Должно быть 6 знаков.")]
        public string ZipCode { get; set; }
        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Должно быть 50 знаков.")]
        public string Region { get; set; }
        [Display(Name = "Район"),
            StringLength(50, ErrorMessage = "Должно быть 50 знаков.")]
        public string District { get; set; }
        [Display(Name = "Населенный пункт"),
            StringLength(50, ErrorMessage = "Должно быть 50 знаков.")]
        public string City { get; set; }
        [Display(Name = "Улица"),
            StringLength(50, ErrorMessage = "Должно быть 50 знаков.")]
        public string Street { get; set; }
        [Display(Name = "Дом"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string StreetNumber { get; set; }
        [Display(Name = "Корпус/строение"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string Building { get; set; }
        [Display(Name = "Квартира"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string Appartment { get; set; }
        [Display(Name = "Заграничный паспорт: серия"),
            StringLength(20, ErrorMessage = "Должно быть 20 знаков.")]
        public string InternationalPassportSeries { get; set; }
        [Display(Name = "Заграничный паспорт: номер"),
            StringLength(20, ErrorMessage = "Должно быть 20 знаков.")]
        public string InternationalPassportNumber { get; set; }
        [Display(Name = "Заграничный паспорт: дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InternationalPassportDateOfIssue { get; set; }
        [Display(Name = "Заграничный паспорт: кем выдан"),
            StringLength(50, ErrorMessage = "Должно быть 50 знаков.")]
        public string InternationalPassportIssuedBy { get; set; }
    }
}
