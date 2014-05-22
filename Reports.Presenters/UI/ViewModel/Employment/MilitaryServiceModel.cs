using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class MilitaryServiceModel
    {
        [Display(Name = "Военнообязанный")]
        public bool IsLiableForMilitaryService { get; set; }

        [Display(Name = "Номер военного билета"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string MilitaryCardNumber { get; set; }

        [Display(Name = "Дата"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MilitaryCardDate { get; set; }

        [Display(Name = "Категория запаса")]
        public int ReserveCategory { get; set; }
        public IEnumerable<SelectListItem> ReserveCategoryItems { get; set; }

        [Display(Name = "Воинское звание")]
        public int Rank { get; set; }
        public IEnumerable<SelectListItem> RankItems { get; set; }

        // Состав (профиль)
        [Display(Name = "Состав (профиль)")]
        public int SpecialityCategory { get; set; }
        public IEnumerable<SelectListItem> SpecialityCategoryItems { get; set; }

        [Display(Name = "Полное кодовое обозначение ВУС"),
            StringLength(6, ErrorMessage = "Не более 6 знаков.")]
        public string MilitarySpecialityCode { get; set; }

        [Display(Name = "Категория годности к военной службе"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string CombatFitness { get; set; }

        [Display(Name = "Наименование ВК по месту жительства"),
            StringLength(100, ErrorMessage = "Не более 100 знаков.")]
        public string Commissariat { get; set; }

        // Состоит на воинском учете?


    }
}
