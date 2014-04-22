using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class PersonnelManagersModel
    {
        [Display(Name = "Дата приказа о приеме"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EmploymentOrderDate { get; set; }

        [Display(Name = "Номер приказа о приеме"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string EmploymentOrderNumber { get; set; }

        [Display(Name = "Принять на работу с даты"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EmploymentDate { get; set; }

        [Display(Name = "Дата ТД"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ContractDate { get; set; }

        [Display(Name = "Номер ТД"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string ContractNumber { get; set; }

        [Display(Name = "Северная надбавка"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string NorthernAreaAddition { get; set; }

        [Display(Name = "Районный коэффициент"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string AreaMultiplier { get; set; }

        [Display(Name = "Территориальная надбавка"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string AreaAddition { get; set; }

        // Грейд

        [Display(Name = "лет"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string InsurableExperienceYears { get; set; }

        [Display(Name = "месяцев"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string InsurableExperienceMonths { get; set; }

        [Display(Name = "дней"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string InsurableExperienceDays { get; set; }

        [Display(Name = "лет"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string NorthExperienceYears { get; set; }

        [Display(Name = "месяцев"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string NorthExperienceMonths { get; set; }

        [Display(Name = "дней"),
            StringLength(2, ErrorMessage = "Не более 2 знаков.")]
        public string NorthExperienceDays { get; set; }

        // Ознакомлен с регламентными документами

        [Display(Name = "Лицевой счет"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string PersonalAccount { get; set; }

        // Заявление на вычет

        [Display(Name = "Доход с предыдущего места работы"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string PreviousIncome { get; set; }

        [Display(Name = "Статус нерезидента")]
        public bool IsNonResident { get; set; }
    }
}
