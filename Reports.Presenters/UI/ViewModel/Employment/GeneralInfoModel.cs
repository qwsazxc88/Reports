using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class GeneralInfoModel
    {
        [Display(Name = "Фамилия"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            RegularExpression(@"^[А-Яа-я]([А-Яа-я]|-|'| ){0,48}[А-Яа-я]$", ErrorMessage = "Недопустимый формат"),
            Required(ErrorMessage = "Обязательное поле")]
        public string LastName { get; set; }
        [Display(Name = "Имя"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            RegularExpression(@"^[А-Яа-я]([А-Яа-я]|-|'| ){0,48}[А-Яа-я]$", ErrorMessage = "Недопустимый формат"),
            Required(ErrorMessage = "Обязательное поле")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            RegularExpression(@"^[А-Яа-я]([А-Яа-я]|-|'| ){0,48}[А-Яа-я]$", ErrorMessage = "Недопустимый формат"),]
        public string Patronymic { get; set; }
        [Display(Name = "Отчество отсутствует")]
        public bool IsPatronymicAbsent { get; set; }

        [Display(Name = "Если изменяли ФИО, укажите их, а также когда меняли, где и по какой причине")]
        public IList<NameChangeDto> NameChanges { get; set; }

        [Display(Name = "Пол")]
        public bool IsMale { get; set; }
        
        [Display(Name = "Гражданство"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Citizenship { get; set; }

        [Display(Name = "Вид застрахованного лица"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string InsuredPersonType { get; set; }

        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string RegionOfBirth { get; set; }
        [Display(Name = "Район"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string DistrictOfBirth { get; set; }
        [Display(Name = "Населенный пункт (город, поселок и т.п.)"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string CityOfBirth { get; set; }

        [Display(Name = "Знание иностранных языков")]
        public IList<ForeignLanguageDto> ForeignLanguages { get; set; }

        [Display(Name = "ИНН №", Prompt = "12 цифр"),
            RegularExpression(@"^\d{12}$", ErrorMessage = "Требуется 12 цифр"),
            Required(ErrorMessage = "Обязательное поле")]
        public string INN { get; set; }
        [Display(Name = "СНИЛС №", Prompt = "###-###-###-##"),
            RegularExpression(@"^(\d{3}-){3}\d{2}$", ErrorMessage = "Требуется формат ###-###-###-##"),
            Required(ErrorMessage = "Обязательное поле")]
        public string SNILS { get; set; }

        [Display(Name = "Инвалидность")]
        public IList<DisabilityDto> Disabilities { get; set; }

        [Display(Name = "Резидент")]
        public bool IsResident { get; set; }

        [Display(Name = "Согласен на обработку своих персональных данных")]
        public bool AgreedToPersonalDataProcessing { get; set; }

        public GeneralInfoModel()
        {
            this.NameChanges = new List<NameChangeDto>();
            this.ForeignLanguages = new List<ForeignLanguageDto>();
            this.Disabilities = new List<DisabilityDto>();
        }
    }
}
