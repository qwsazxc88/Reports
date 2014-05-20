using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class BackgroundCheckModel
    {
        [Display(Name = "Размер Вашей среднемесячной заработной платы по последнему месту работы")]
        public int? AverageSalary { get; set; }

        [Display(Name = "Имеете ли Вы какие-либо финансовые обязательства (закладные, ссуды, кредиты и т.д.), какие, перед кем, срок погашения")]
        public IList<string> Liabilities { get; set; }

        [Display(Name = "Причина смены последнего места работы")]
        public string PreviousDismissalReason { get; set; }

        [Display(Name = "ФИО и контактный телефон непосредственного руководителя или руководителя кадровой службы по прежнему месту работы")]
        public string PreviousSuperior { get; set; }

        [Display(Name = "На какую должность претендуете")]
        public string PositionSought { get; set; }

        [Display(Name = "Участие в войнах, каких-либо боевых действиях, ликвидации аварий, катастроф и стихийных бедствий")]
        public string MilitaryOperationsExperience { get; set; }

        [Display(Name = "Имеете ли Вы водительское удостоверение")]
        public bool HasDriversLicense { get; set; }

        [Display(Name = "Номер")]
        public string DriversLicenseNumber { get; set; }

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DriversLicenseDateOfIssue { get; set; }

        [Display(Name = "Категории")]
        public string DriversLicenseCategories { get; set; }

        [Display(Name = "Водительский стаж")]
        public int? DrivingExperience { get; set; }

        [Display(Name = "Имеете ли Вы автомобиль")]
        public bool HasAutomobile { get; set; }

        [Display(Name = "Марка")]
        public string AutomobileMake { get; set; }

        [Display(Name = "Государственный регистрационный знак")]
        public string AutomobileLicensePlateNumber { get; set; }

        [Display(Name = "Имеете ли Вы возможность выезжать в служебные командировки")]
        public bool IsReadyForBusinessTrips { get; set; }

        [Display(Name = "Какими видами спорта Вы занимаетесь (занимались ранее), имеете ли спортивные разряды и/или звания")]
        public string Sports { get; set; }

        [Display(Name = "Ваши увлечения")]
        public string Hobbies { get; set; }

        [Display(Name = "Назовите одно или несколько наиболее важных событий (на Ваш взгляд), произошедших в Вашей жизни за последние 3-5 лет")]
        public string ImportantEvents { get; set; }

        [Display(Name = "Рекомендации должностных лиц, знающих Вас по предыдущим местам работы, или рекомендации кого-либо из сотрудников нашего банка")]
        public IList<ReferenceDto> References { get; set; }

        [Display(Name = "Наличие хронических заболеваний (по желанию, если есть, то перечислите)")]
        public string ChronicalDiseases { get; set; }

        public BackgroundCheckModel()
        {
            this.Liabilities = new List<string>();
            this.References = new List<ReferenceDto>();
        }
    }
}
