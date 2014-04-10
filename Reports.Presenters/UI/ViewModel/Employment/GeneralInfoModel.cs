using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class GeneralInfoModel
    {
        [Display(Name = "Фамилия"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string LastName { get; set; }
        [Display(Name = "Имя"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string FirstName { get; set; }
        [Display(Name = "Отчество"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string Patronymic { get; set; }
        [Display(Name = "Если изменяли ФИО, укажите когда, где и по какой причине"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
            public string NameChange { get; set; }

        // TODO: Gender
        //[Display(Name = "Пол")]
        //public string Gender { get; set; }
        
        [Display(Name = "Гражданство"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string Citizenship { get; set; }
        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public DateTime DateOfBirth { get; set; }
        [Display(Name = "Место рождения"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string BirthPlace { get; set; }

        [Display(Name = "Страна"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string Country { get; set; }
        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string Region { get; set; }
        [Display(Name = "Населенный пункт"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string City { get; set; }

        // TODO: Иностранные языки

        [Display(Name = "ИНН №"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string INN { get; set; }
        [Display(Name = "СНИЛС №"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string SNILS { get; set; }

        [Display(Name = "Инвалидность"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
            public string Disabilities { get; set; }

        [Display(Name = "Резидент")]
            public bool IsResident { get; set; }

        [Display(Name = "Согласен на обработку своих персональных данных")]
            public bool AgreedToPersonalDataProcessing { get; set; }
    }
}
