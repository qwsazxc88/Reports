﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class BackgroundCheckModel : AbstractEmploymentModel
    {
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
        public DateTime DriversLicenseDateOfIssue { get; set; } //ok

        [Display(Name = "Категории")]
        public string DriversLicenseCategories { get; set; } // TODO: multiselect dropdown

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

        public BackgroundCheckModel()
        {
            this.Version = 0;
            this.References = new List<ReferenceDto>();
        }
    }
}