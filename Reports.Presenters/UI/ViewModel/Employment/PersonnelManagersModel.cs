using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PersonnelManagersModel : AbstractEmploymentModel
    {
        [Display(Name = "Дата приказа о приеме"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentOrderDate { get; set; }

        [Display(Name = "Номер приказа о приеме"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string EmploymentOrderNumber { get; set; }

        [Display(Name = "Принять на работу с даты"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentDate { get; set; }

        [Display(Name = "Дата ТД"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ContractDate { get; set; }

        [Display(Name = "Номер ТД"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string ContractNumber { get; set; }

        [Display(Name = "Северная надбавка")]
        public decimal? NorthernAreaAddition { get; set; }

        [Display(Name = "Районный коэффициент")]
        public decimal? AreaMultiplier { get; set; }

        [Display(Name = "Территориальная надбавка")]
        public decimal? AreaAddition { get; set; }

        [Display(Name = "Надбавка за разъездной характер работы")]
        public decimal? TravelRelatedAddition { get; set; }

        [Display(Name = "Надбавка за квалификацию")]
        public decimal? CompetenceAddition { get; set; }

        [Display(Name = "Надбавка за стаж работы специалистом фронт-офиса")]
        public decimal? FrontOfficeExperienceAddition { get; set; }

        [Display(Name = "Грейд")]
        public int? Grade { get; set; }

        [Display(Name = "лет")]
        public int OverallExperienceYears { get; set; }

        [Display(Name = "месяцев")]
        public int OverallExperienceMonths { get; set; }

        [Display(Name = "дней")]
        public int OverallExperienceDays { get; set; }

        [Display(Name = "лет")]
        public int InsurableExperienceYears { get; set; }

        [Display(Name = "месяцев")]
        public int InsurableExperienceMonths { get; set; }

        [Display(Name = "дней")]
        public int InsurableExperienceDays { get; set; }        

        // Ознакомлен с регламентными документами

        [Display(Name = "Номер лицевого счета"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string PersonalAccount { get; set; }

        [Display(Name = "Контрагент лицевого счета"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string PersonalAccountContractor { get; set; }
        
        // Признаки ЦФО 1
        // Признаки ЦФО 2
        // Заявление на вычет
        // Скан заявления на вычет
        // Группа доступа

        [Display(Name = "Оформлен прием. Кадровик"),
            StringLength(50, ErrorMessage = "Не более 100 знаков.")]
        public string ApprovedByPersonnelManager { get; set; }

        public PersonnelManagersModel()
        {
            this.Version = 0;
        }
    }
}
