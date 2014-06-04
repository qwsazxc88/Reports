using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ManagersModel : AbstractEmploymentModel
    {
        // TODO EMPL Position
        // TODO EMPL Directorate
        // TODO EMPL Department
        // TODO EMPL EmploymentConditions
        // TODO EMPL Schedule

        [Display(Name = "Испытательный срок"),
            StringLength(50, ErrorMessage = "Не более 200 знаков.")]
        public string ProbationaryPeriod { get; set; } //ok

        /*
        [Display(Name = "Оклад"),
            StringLength(20, ErrorMessage = "Не более 200 знаков.")]
        public string Salary { get; set; }*/

        [Display(Name = "Место работы (нас. пункт)"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string WorkCity { get; set; } //ok

        [Display(Name = "Персональная надбавка"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public decimal? PersonalAddition { get; set; } //ok

        [Display(Name = "Должностная надбавка"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public decimal? PositionAddition { get; set; } //ok

        [Display(Name = "Фронт/Бэк")]
        public bool IsFront { get; set; } //ok

        [Display(Name = "Размер премии"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public decimal? Bonus { get; set; } //ok

        [Display(Name = "Материальная ответственность")]
        public bool IsLiable { get; set; } //ok

        [Display(Name = "Номер заявки в службе подбора персонала"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string RequestNumber { get; set; } //ok

        public ManagersModel()
        {
            this.Version = 0;
        }
    }
}
