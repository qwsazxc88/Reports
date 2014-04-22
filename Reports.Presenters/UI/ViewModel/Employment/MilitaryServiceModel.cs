using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment
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

        // категория запаса

        // Воинское звание

        // Состав (профиль)

        [Display(Name = "Полное кодовое обозначение ВУС"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
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
