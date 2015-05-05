using System;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ContactsModel : AbstractEmploymentModel
    {
        [Display(Name = "Страна"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Country { get; set; } //ok
        [Display(Name = "Почтовый индекс"),
            StringLength(6, ErrorMessage = "Требуется 6 знаков.")]
        public string ZipCode { get; set; } //ok
        [Display(Name = "Республика/Край"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Republic { get; set; } //ok
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
        [Display(Name = "Телефон рабочий", Prompt = "+#(###) ###-##-##"),
            StringLength(20, ErrorMessage = "Должно быть не более 20 знаков."),
            RegularExpression(@"^\S\d{1}\S\d{3}\S\s\d{3}\S\d{2}\S\d{2}$", ErrorMessage = "Требуется ввести номер телефона в формате +#(###) ###-##-##")]
        public string WorkPhone { get; set; } //ok
        [Display(Name = "Телефон домашний"),
            StringLength(20, ErrorMessage = "Должно быть не более 20 знаков."),
            RegularExpression(@"^\S\d{1}\S\d{3}\S\s\d{3}\S\d{2}\S\d{2}$", ErrorMessage = "Требуется ввести номер телефона в формате +#(###) ###-##-##")]
        public string HomePhone { get; set; } //ok
        [Display(Name = "Телефон мобильный"),
            DataType(DataType.PhoneNumber),
            StringLength(20, ErrorMessage = "Должно быть не более 20 знаков."),
            RegularExpression(@"^\S\d{1}\S\d{3}\S\s\d{3}\S\d{2}\S\d{2}$", ErrorMessage = "Требуется ввести номер телефона в формате +#(###) ###-##-##")]
        public string Mobile { get; set; } //ok
        [Display(Name = "E-mail"),
            DataType(DataType.EmailAddress),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Email { get; set; } //ok

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok

        public bool IsContactDraft { get; set; } //ok

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public ContactsModel()
        {
            this.Version = 0;
        }
    }
}
