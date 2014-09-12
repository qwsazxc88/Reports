﻿using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class RosterModel
    {
        public bool IsCandidateInfoAvailable { get; set; }
        public bool IsBackgroundCheckAvailable { get; set; }
        public bool IsManagersAvailable { get; set; }
        public bool IsPersonalManagersAvailable { get; set; }

        [Display(Name = "Реестр по приему")]
        public IList<CandidateDto> Roster { get; set; }

        // Фильтры
        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "Статус")]
        public int? StatusId { get; set; } //ok
        public IEnumerable<SelectListItem> Statuses { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        // Согласование списком для руководителя
        public bool IsBulkApproveByManagerAvailable { get; set; }

        // Согласование списком для вышестоящего руководителя
        public bool IsBulkApproveByHigherManagerAvailable { get; set; }

        public RosterModel()
        {
            this.Roster = new List<CandidateDto>();
        }
    }
}
