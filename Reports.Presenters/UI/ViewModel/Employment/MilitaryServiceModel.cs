using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class MilitaryServiceModel : AbstractEmploymentModel
    {
        [Display(Name = "Военнообязанный")]
        public bool IsLiableForMilitaryService { get; set; } //ok

        [Display(Name = "Номер военного билета"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string MilitaryCardNumber { get; set; } //ok

        [Display(Name = "Дата"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MilitaryCardDate { get; set; } //ok

        [Display(Name = "Категория запаса"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string ReserveCategory { get; set; } //ok

        [Display(Name = "Воинское звание")]
        public int? Rank { get; set; } //ok
        public IEnumerable<SelectListItem> RankItems { get; set; }

        [Display(Name = "Состав (профиль)")]
        public string SpecialityCategory { get; set; } //ok

        [Display(Name = "Полное кодовое обозначение ВУС"),
            StringLength(7, ErrorMessage = "Не более 7 знаков.")]
        public string MilitarySpecialityCode { get; set; } //ok

        [Display(Name = "Категория годности к военной службе"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string CombatFitness { get; set; } //ok

        [Display(Name = "Наименование ВК по месту жительства"),
            StringLength(100, ErrorMessage = "Не более 100 знаков.")]
        public string Commissariat { get; set; } //ok

        [Display(Name = "Состоит на воинском учете"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string MilitaryServiceRegistrationInfo { get; set; } //ok

        [Display(Name = "а) общем (номер команды, партии)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string CommonMilitaryServiceRegistrationInfo { get; set; }

        [Display(Name = "б) специальном"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string SpecialMilitaryServiceRegistrationInfo { get; set; }

        [Display(Name = "Отметка о снятии с ВУ")]
        public int? RegistrationExpiration { get; set; } //ok
        public IEnumerable<SelectListItem> RegistrationExpirationItems { get; set; }

        [Display(Name = "Бронирован")]
        public bool IsReserved { get; set; }

        [Display(Name = "Номер мобилизационного талона"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string MobilizationTicketNumber { get; set; }

        [Display(Name = "Категория персонала")]
        public int? PersonnelCategory { get; set; } //ok
        public IEnumerable<SelectListItem> PersonnelCategoryItems { get; set; }

        [Display(Name = "Тип")]
        public int? PersonnelType { get; set; } //ok
        public IEnumerable<SelectListItem> PersonnelTypeItems { get; set; }

        [Display(Name = "Предписание")]
        public bool IsAssigned { get; set; } //ok

        [Display(Name = "Призыв на военную службу")]
        public int? ConscriptionStatus { get; set; } //ok
        public IEnumerable<SelectListItem> ConscriptionStatusItems { get; set; }

        public MilitaryServiceModel()
        {
            this.Version = 0;
        }

    }
}
