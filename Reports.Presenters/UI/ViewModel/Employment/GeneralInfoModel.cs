using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class GeneralInfoModel : AbstractEmploymentModel
    {
        [Display(Name = "Фамилия"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            RegularExpression(@"^[А-Яа-я]([А-Яа-я]|-|'| ){0,48}[А-Яа-я]$", ErrorMessage = "Недопустимый формат"),
            Required(ErrorMessage = "*")]
        public string LastName { get; set; } //ok
        [Display(Name = "Имя"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            RegularExpression(@"^[А-Яа-я]([А-Яа-я]|-|'| ){0,48}[А-Яа-я]$", ErrorMessage = "Недопустимый формат"),
            Required(ErrorMessage = "*")]
        public string FirstName { get; set; } //ok
        [Display(Name = "Отчество"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            RegularExpression(@"^[А-Яа-я]([А-Яа-я]|-|'| ){0,48}[А-Яа-я]$", ErrorMessage = "Недопустимый формат"),]
        public string Patronymic { get; set; } //ok
        [Display(Name = "Отчество отсутствует")]
        public bool IsPatronymicAbsent { get; set; } //ok

        [Display(Name = "Если изменяли ФИО, укажите их, а также когда меняли, где и по какой причине")]
        public IList<NameChangeDto> NameChanges { get; set; } //ok
        
        [Display(Name = "Пол")]
        public bool IsMale { get; set; } //ok
        
        [Display(Name = "Гражданство"),
            Required(ErrorMessage = "*")]
        public int CitizenshipId { get; set; } //ok
        public IEnumerable<SelectListItem> CitizenshipItems { get; set; }
        
        [Display(Name = "Вид застрахованного лица")]
        public int? InsuredPersonTypeId { get; set; } //ok
        public IEnumerable<SelectListItem> InsuredPersonTypeItems { get; set; }
        public string InsuredPersonTypeSelectedName { get; set; }
        
        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "*")]
        public DateTime? DateOfBirth { get; set; } //ok

        [Display(Name = "Область"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string RegionOfBirth { get; set; } //ok
        [Display(Name = "Район"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string DistrictOfBirth { get; set; } //ok
        [Display(Name = "Населенный пункт (город, поселок и т.п.)"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string CityOfBirth { get; set; } //ok
        
        [Display(Name = "Знание иностранных языков")]
        public IList<ForeignLanguageDto> ForeignLanguages { get; set; } //TODO: EMPL кнопка добавления

        [Display(Name = "ИНН №", Prompt = "12 цифр"),
            RegularExpression(@"^\d{12}$", ErrorMessage = "Требуется 12 цифр")]
        public string INN { get; set; } //ok
        [Display(Name = "СНИЛС №", Prompt = "###-###-###-##"),
            RegularExpression(@"^(\d{3}-){3}\d{2}$", ErrorMessage = "Требуется формат ###-###-###-##"),
            Required(ErrorMessage = "*")]
        public string SNILS { get; set; } //ok

        // Инвалидность

        [Display(Name = "Серия справки")]
        public string DisabilityCertificateSeries { get; set; }

        [Display(Name = "Номер справки")]
        public string DisabilityCertificateNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DisabilityCertificateDateOfIssue { get; set; }

        [Display(Name = "Группа инвалидности")]
        public int? DisabilityDegreeId { get; set; }
        public IEnumerable<SelectListItem> DisabilityDegrees { get; set; }

        [Display(Name = "Срок действия справки"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DisabilityCertificateExpirationDate { get; set; }

        // ------------

        [Display(Name = "Статус"),
            Required(ErrorMessage = "*")]
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> StatusItems { get; set; } //ok
        
        [Display(Name = "Согласен на обработку своих персональных данных")]
        public bool AgreedToPersonalDataProcessing { get; set; } //ok

        public HttpPostedFileBase PhotoFile { get; set; }
        public HttpPostedFileBase INNScanFile { get; set; }
        public HttpPostedFileBase SNILSScanFile { get; set; }
        public HttpPostedFileBase DisabilityCertificateScanFile { get; set; }

        public string PhotoAttachmentFilename { get; set; }
        public int PhotoAttachmentId { get; set; }
        public string INNScanAttachmentFilename { get; set; }
        public int INNScanAttachmentId { get; set; }
        public string SNILSScanAttachmentFilename { get; set; }
        public int SNILSScanAttachmentId { get; set; }
        public string DisabilityCertificateScanAttachmentFilename { get; set; }
        public int DisabilityCertificateScanAttachmentId { get; set; }
                
        public GeneralInfoModel()
        {
            this.Version = 0;
            this.NameChanges = new List<NameChangeDto>();            
            this.ForeignLanguages = new List<ForeignLanguageDto>();        
        }
    }
}