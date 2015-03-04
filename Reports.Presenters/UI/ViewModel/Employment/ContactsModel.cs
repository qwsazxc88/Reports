using System;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ContactsModel : AbstractEmploymentModel
    {
        [Display(Name = "Почтовый индекс"),
            StringLength(6, ErrorMessage = "Требуется 6 знаков.")]
        public string ZipCode { get; set; } //ok
        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Region { get; set; } //ok
        [Display(Name = "Район"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string District { get; set; } //ok
        [Display(Name = "Населенный пункт"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string City { get; set; } //ok
        [Display(Name = "Улица"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Street { get; set; } //ok
        [Display(Name = "Дом"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string StreetNumber { get; set; } //ok
        [Display(Name = "Корпус/строение"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string Building { get; set; } //ok
        [Display(Name = "Квартира"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string Apartment { get; set; } //ok
        [Display(Name = "Телефон рабочий"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string WorkPhone { get; set; } //ok
        [Display(Name = "Телефон домашний"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string HomePhone { get; set; } //ok
        [Display(Name = "Телефон мобильный"),
            DataType(DataType.PhoneNumber),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string Mobile { get; set; } //ok
        [Display(Name = "E-mail"),
            DataType(DataType.EmailAddress),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Email { get; set; } //ok

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public ContactsModel()
        {
            this.Version = 0;
        }
    }
}
