using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class MilitaryServiceModel
    {
        [Display(Name = "Военнообязанный")]
        public bool IsLiableForMilitaryService { get; set; }
        // номер ВБ
        // дата
        // категория запаса
        // Воинское звание
        // Состав (профиль)
        // Полное кодовое обозначение ВУС
        [Display(Name = "Категория годности к военной службе"),
        // ??? too long?
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string CombatFitness { get; set; }
        [Display(Name = "Наименование ВК по месту жительства"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Commissariat { get; set; }
    }
}
