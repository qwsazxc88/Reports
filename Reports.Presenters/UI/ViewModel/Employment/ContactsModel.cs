using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class ContactsModel
    {
        [Display(Name = "Почтовый индекс"),
            StringLength(6, ErrorMessage = "Не более 6 знаков.")]
        public string ZipCode { get; set; }
        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
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
        [Display(Name = "Телефон рабочий"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string WorkPhone { get; set; }
        [Display(Name = "Телефон домашний"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string HomePhone { get; set; }
        [Display(Name = "Телефон мобильный"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string Mobile { get; set; }
        [Display(Name = "E-mail"),
            StringLength(10, ErrorMessage = "Должно быть 10 знаков.")]
        public string Email { get; set; }
    }
}
