using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ManagersModel : AbstractEmploymentModel
    {
        [Display(Name = "Должность"),
            Required(ErrorMessage = "Обязательное поле")]
        public int PositionId { get; set; }
        public IEnumerable<SelectListItem> PositionItems { get; set; }

        [Display(Name = "Дирекция"),
            Required(ErrorMessage = "Обязательное поле")]
        public int DirectorateId { get; set; }
        public IEnumerable<SelectListItem> DirectorateItems { get; set; }

        [Display(Name = "Структурное подразделение")]
        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem> DepartmentItems { get; set; }

        [Display(Name = "Условия приема на работу"),
            StringLength(100, ErrorMessage = "Не более 100 знаков.")]
        public string EmploymentConditions { get; set; } //ok

        [Display(Name = "График работы"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Schedule { get; set; } //ok

        [Display(Name = "Испытательный срок"),
            StringLength(50, ErrorMessage = "Не более 200 знаков.")]
        public string ProbationaryPeriod { get; set; } //ok

        [Display(Name = "Оклад по дням")]
        public decimal? DailySalaryBasis { get; set; }

        [Display(Name = "Оклад по часам")]
        public decimal? HourlySalaryBasis { get; set; }

        [Display(Name = "Ставка")]
        public decimal? SalaryMultiplier { get; set; }

        [Display(Name = "Место работы по ТД (нас. пункт)"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string WorkCity { get; set; } //ok

        [Display(Name = "Персональная надбавка")]
        public decimal? PersonalAddition { get; set; } //ok

        [Display(Name = "Должностная надбавка")]
        public decimal? PositionAddition { get; set; } //ok

        [Display(Name = "Фронт/Бэк")]
        public bool IsFront { get; set; } //ok

        [Display(Name = "Размер премии")]
        public decimal? Bonus { get; set; } //ok

        [Display(Name = "Материальная ответственность")]
        public bool IsLiable { get; set; } //ok

        [Display(Name = "Номер заявки в службе подбора персонала"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string RequestNumber { get; set; } //ok

        public bool IsEditable { get; set; }

        [Display(Name = "Согласен на прием (руководитель)")]
        public bool? ManagerApprovalStatus { get; set; }
        public string ApprovingManagerName { get; set; }
        [Display(Name = "Причина отказа")]
        public string ManagerRejectionReason { get; set; }

        [Display(Name = "Согласен на прием (вышестоящий руководитель)")]
        public bool? HigherManagerApprovalStatus { get; set; }
        public string ApprovingHigherManagerName { get; set; }
        [Display(Name = "Причина отказа")]
        public string HigherManagerRejectionReason { get; set; }
        
        public string RejectingChiefName { get; set; }
        [Display(Name = "Причина отказа")]
        public string ChiefRejectionReason { get; set; }

        public ManagersModel()
        {
            this.Version = 0;
        }
    }
}
