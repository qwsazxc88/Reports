using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class GeneralInfoModel
    {
        [Display(Name = "Фамилия"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            // RegularExpression("^[А-Я]{1}[а-я]*$"),
            Required()]
        public string LastName { get; set; }
        [Display(Name = "Имя"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required()]
        public string FirstName { get; set; }
        [Display(Name = "Отчество"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Patronymic { get; set; }
        [Display(Name = "Если изменяли ФИО, укажите их, а также когда меняли, где и по какой причине"),
            StringLength(350, ErrorMessage = "Не более 350 знаков.")]
        public string NameChange { get; set; }

        // TODO: Gender
        //[Display(Name = "Пол")]
        //public string Gender { get; set; }
        
        [Display(Name = "Гражданство"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required()]
        public string Citizenship { get; set; }
        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required()]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Страна"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required()]
        public string CountryOfBirth { get; set; }
        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string RegionOfBirth { get; set; }
        [Display(Name = "Район"),
            StringLength(50, ErrorMessage = "Должно быть 50 знаков.")]
        public string DistrictOfBirth { get; set; }
        [Display(Name = "Населенный пункт (город, поселок и т.п.)"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required()]
        public string CityOfBirth { get; set; }

        // TODO: Иностранные языки

        [Display(Name = "ИНН №"),
            RegularExpression(@"^\d{12}$", ErrorMessage = "Требуется 12 цифр."),
            Required()]
        public string INN { get; set; }
        [Display(Name = "СНИЛС №"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            RegularExpression(@"^(\d{3}-){3}\d{2}$]", ErrorMessage = "Требуется формат xxx-xxx-xxx-xx")]
        public string SNILS { get; set; }

        [Display(Name = "Инвалидность"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Disabilities { get; set; }

        [Display(Name = "Резидент")]
        public bool IsResident { get; set; }

        [Display(Name = "Согласен на обработку своих персональных данных"),
            Required()]
        public bool AgreedToPersonalDataProcessing { get; set; }
    }
}
