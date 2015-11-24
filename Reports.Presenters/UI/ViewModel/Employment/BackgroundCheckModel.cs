using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Web;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class BackgroundCheckModel : AbstractEmploymentModel
    {
        //флажок для сотрудников Экспресс-Волги
        public bool IsVolga { get; set; }

        [Display(Name = "Размер Вашей среднемесячной заработной платы по последнему месту работы")]
        public decimal? AverageSalary { get; set; } //ok

        [Display(Name = "Имеете ли Вы какие-либо финансовые обязательства (закладные, ссуды, кредиты и т.д.), какие, перед кем, срок погашения")]
        public string Liabilities { get; set; } //ok

        [Display(Name = "Причина смены последнего места работы")]
        public string PreviousDismissalReason { get; set; } //ok

        [Display(Name = "ФИО и контактный телефон непосредственного руководителя или руководителя кадровой службы по прежнему месту работы")]
        public string PreviousSuperior { get; set; } //ok

        [Display(Name = "На какую должность претендуете")]
        public string PositionSought { get; set; } //ok

        [Display(Name = "Участие в войнах, каких-либо боевых действиях, ликвидации аварий, катастроф и стихийных бедствий")]
        public string MilitaryOperationsExperience { get; set; } //ok

        [Display(Name = "Имеете ли Вы водительское удостоверение")]
        public bool HasDriversLicense { get; set; } //ok

        [Display(Name = "№")]
        // Формат: 12 АБ 123456
        public string DriversLicenseNumber { get; set; } //ok

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DriversLicenseDateOfIssue { get; set; } //ok

        [Display(Name = "Категории"),
            RegularExpression(@"^((M|[A-D]|BE|CE|DE|Tm|Tb|A1|B1|C1|D1|C1E|D1E)(,| |(, )|$))*$",
                ErrorMessage = "Требуется список категорий, разделенных запятыми или пробелами")]
        public string DriversLicenseCategories { get; set; }

        [Display(Name = "Водительский стаж")]
        public int? DrivingExperience { get; set; } //ok

        [Display(Name = "Имеете ли Вы автомобиль")]
        public bool HasAutomobile { get; set; } //ok

        [Display(Name = "Марка")]
        public string AutomobileMake { get; set; } //ok

        [Display(Name = "Государственный регистрационный знак")]
        // Формат: А123АБ 12[3]
        public string AutomobileLicensePlateNumber { get; set; } //ok

        [Display(Name = "Имеете ли Вы возможность выезжать в служебные командировки")]
        public bool IsReadyForBusinessTrips { get; set; } //ok

        [Display(Name = "Какими видами спорта Вы занимаетесь (занимались ранее), имеете ли спортивные разряды и/или звания")]
        public string Sports { get; set; } //ok

        [Display(Name = "Ваши увлечения")]
        public string Hobbies { get; set; } //ok

        [Display(Name = "Назовите одно или несколько наиболее важных событий (на Ваш взгляд), произошедших в Вашей жизни за последние 3-5 лет")]
        public string ImportantEvents { get; set; } //ok

        [Display(Name = "Рекомендации должностных лиц, знающих Вас по предыдущим местам работы, или рекомендации кого-либо из сотрудников нашего банка")]
        public IList<ReferenceDto> References { get; set; } //ok

        [Display(Name = "Наличие хронических заболеваний (по желанию, если есть, то перечислите)")]
        public string ChronicalDiseases { get; set; } //ok

        [Display(Name = "Привлекались ли Вы к уголовной, административной или иной ответственности, когда, за что, мера наказания")]
        public string Penalties { get; set; } //ok

        [Display(Name = "Состоите ли Вы на психиатрическом или наркологическом учете (если да, то где)")]
        public string PsychiatricAndAddictionTreatment { get; set; }

        [Display(Name = "Ваше отношение к табакокурению")]
        public string Smoking { get; set; }

        [Display(Name = "Ваше отношение к алкоголю")]
        public string Drinking { get; set; }

        [Display(Name = "Являетесь ли Вы или Ваши родственники акционерами (владельцами долей) предприятий и организаций")]
        public string OwnerOfShares { get; set; }

        [Display(Name = "Занимаете ли Вы или Ваши родственники должности в органах управления иных юридических лиц")]
        public string PositionInGoverningBodies { get; set; }

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok

        public bool IsBGDraft { get; set; } //ok

        public bool IsApproveBySecurityAvailable { get; set; }

        [Display(Name = "Без проверки")]
        public bool IsApprovalSkipped { get; set; }
        [Display(Name = "Согласование")]        
        public bool? ApprovalStatus { get; set; }
        public IEnumerable<SelectListItem> ApprovalStatuses { get; set; }
        public string ApproverName { get; set; }
        [Display(Name = "Дата согласования")]        
        public DateTime? ApprovalDate { get; set; }

        public HttpPostedFileBase PersonalDataProcessingScanFile { get; set; }
        public HttpPostedFileBase InfoValidityScanFile { get; set; }

        public string PersonalDataProcessingScanAttachmentFilename { get; set; }
        public int PersonalDataProcessingScanAttachmentId { get; set; }
        public string InfoValidityScanAttachmentFilename { get; set; }
        public int InfoValidityScanAttachmentId { get; set; }
        public int RowID { get; set; }

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        [Display(Name = "Текст комментария"),
            StringLength(256, ErrorMessage = "Не более 256 знаков.")]
        public string Comment { get; set; } //ok
        public IList<EmploymentCandidateCommentDto> Comments { get; set; }
        public bool IsAddCommentAvailable { get; set; }

        [Display(Name = "Ссылка на задачу в системе Pyrus")]
        public string PyrusRef { get; set; } //ok

        public bool IsCancelApproveAvailale { get; set; }//для показа консультатнам кнопки для отмены согласования

        public DateTime? SendTo1C { get; set; }
        public bool IsPrintButtonAvailable { get; set; }
        public bool IsDeleteScanButtonAvailable { get; set; }

        //личный листок по учету кадров
        public HttpPostedFileBase EmploymentFile { get; set; }
        public string EmploymentFileName { get; set; }
        public int EmploymentFileId { get; set; }
        public int DeleteAttachmentId { get; set; }

        public BackgroundCheckModel()
        {
            this.Version = 0;
            this.References = new List<ReferenceDto>();
        }
    }
}
