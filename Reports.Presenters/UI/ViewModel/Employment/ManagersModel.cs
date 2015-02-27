﻿using System;
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

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }        

        [Display(Name = "Условия приема на работу"),
            StringLength(100, ErrorMessage = "Не более 100 знаков.")]
        public string EmploymentConditions { get; set; } //ok

        [Display(Name = "Вид занятости")]
        public bool IsSecondaryJob { get; set; } //ok

        [Display(Name = "График работы")]
        public int? ScheduleId { get; set; }
        public IEnumerable<SelectListItem> Schedules { get; set; }

        [Display(Name = "Испытательный срок"),
            StringLength(50, ErrorMessage = "Не более 200 знаков.")]
        public string ProbationaryPeriod { get; set; } //ok

        [Display(Name = "Базовый должностной оклад")]
        public decimal? SalaryBasis { get; set; }

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
                
        public string RejectingChiefName { get; set; }
        [Display(Name = "Причина отказа")]
        public string ChiefRejectionReason { get; set; }

        [Display(Name = "Согласование (Руководитель)")]
        public bool? ManagerApprovalStatus { get; set; }
        public string ApprovingManagerName { get; set; }
        public DateTime? ManagerApprovalDate { get; set; }

        [Display(Name = "Согласование (Вышестоящий руководитель)")]
        public bool? HigherManagerApprovalStatus { get; set; }
        public string ApprovingHigherManagerName { get; set; }
        public DateTime? HigherManagerApprovalDate { get; set; }

        public IEnumerable<SelectListItem> ApprovalStatuses { get; set; }

        [Display(Name = "Причина отказа")]
        public string ManagerRejectionReason { get; set; }

        [Display(Name = "Причина отказа")]
        public string HigherManagerRejectionReason { get; set; }

        public bool IsApproveByManagerAvailable { get; set; }
        public bool IsApproveByHigherManagerAvailable { get; set; }

        //состояние кандидата
        public IList<CandidateStateDto> CandidateState { get; set; }
        
        public ManagersModel()
        {
            this.Version = 0;
        }
    }
}
