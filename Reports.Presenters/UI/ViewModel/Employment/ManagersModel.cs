using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ManagersModel
    {
        [Display(Name = "Испытательный срок"),
            StringLength(20, ErrorMessage = "Не более 200 знаков.")]
        public string ProbationaryPeriod { get; set; }

        [Display(Name = "Оклад"),
            StringLength(20, ErrorMessage = "Не более 200 знаков.")]
        public string Salary { get; set; }

        [Display(Name = "Место работы (нас. пункт)"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string WorkCity { get; set; }

        [Display(Name = "Персональная надбавка"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string PersonalAddition { get; set; }

        [Display(Name = "Должностная надбавка"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string PositionAddition { get; set; }

        [Display(Name = "Фронт/Бэк")]
        public bool IsFront { get; set; }

        [Display(Name = "Размер премии"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Bonus { get; set; }

        [Display(Name = "Материальная ответственность")]
        public bool IsLiable { get; set; }

        [Display(Name = "Номер заявки в службе подбора персонала"),
            StringLength(10, ErrorMessage = "Не более 10 знаков.")]
        public string RequestNumber { get; set; }
    }
}
