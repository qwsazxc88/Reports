using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Web.Mvc;

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

        public bool IsHourlySalaryBasis { get; set; }

        [Display(Name = "Базовый оклад по штатному расписанию (руб)")]
        public decimal? BasicSalary { get; set; }

        [Display(Name = "Северная надбавка (%)")]
        public decimal? NorthernAreaAddition { get; set; }

        [Display(Name = "Районный коэффициент (%)")]
        public decimal? AreaMultiplier { get; set; }

        [Display(Name = "Территориальная надбавка (руб)")]
        public decimal? AreaAddition { get; set; }

        [Display(Name = "Надбавка за разъездной характер работы (руб)")]
        public decimal? TravelRelatedAddition { get; set; }

        [Display(Name = "Надбавка за квалификацию (руб)")]
        public decimal? CompetenceAddition { get; set; }

        [Display(Name = "Надбавка за стаж работы специалистом фронт-офиса (руб)")]
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
            Required(ErrorMessage = "Обязательное поле")]
        public int PersonalAccountContractorId { get; set; }
        public IEnumerable<SelectListItem> PersonalAccountContractors { get; set; }
        
        // Признаки ЦФО 1
        // Признаки ЦФО 2
        // Заявление на вычет
        // Скан заявления на вычет

        [Display(Name = "Группа доступа"),
            Required(ErrorMessage = "*")]
        public int AccessGroupId { get; set; }
        public IEnumerable<SelectListItem> AccessGroups { get; set; }

        [Display(Name = "Ранг"),
            Required(ErrorMessage = "*"),
            Range(2, 7, ErrorMessage = "Требуется число от 2 до 7")]
        public int? Level { get; set; }

        public bool? IsFixedTermContract { get; set; }
        public bool IsContractChangedToIndefinite { get; set; }

        [Display(Name = "Окончание ТД"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ContractEndDate { get; set; }

        [Display(Name = "Дата ДС"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SupplementaryAgreementCreateDate { get; set; }

        [Display(Name = "Номер ДС")]
        public int? SupplementaryAgreementNumber { get; set; }

        [Display(Name = "Дата приказа"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChangeContractToIndefiniteOrderCreateDate { get; set; }

        [Display(Name = "Номер приказа")]
        public int? ChangeContractToIndefiniteOrderNumber { get; set; }

        [Display(Name = "Оформлен прием. Кадровик"),
            StringLength(50, ErrorMessage = "Не более 100 знаков.")]
        public string ApprovedByPersonnelManager { get; set; }

        [Display(Name = "Подписант"),
            Required(ErrorMessage = "*")]
        public int SignerId { get; set; }
        public IEnumerable<SelectListItem> Signers { get; set; }

        [Display(Name = "Вид застрахованного лица")]
        public int? InsuredPersonTypeId { get; set; } //ok
        public IEnumerable<SelectListItem> InsuredPersonTypeItems { get; set; }
        public string InsuredPersonTypeSelectedName { get; set; }

        [Display(Name = "Статус"),
            Required(ErrorMessage = "*")]
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> StatusItems { get; set; } //ok

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public string MessageStr { get; set; }

        public PersonnelManagersModel()
        {
            this.Version = 0;
        }
    }
}
