﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class ChildVacationListModel
    {
        public IList<IdNameDto> Positions;
        public IList<IdNameDto> RequestStatuses;
        public int UserId { get; set; }


        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        //[AutoComplete("AutoComplete", "Departments", "searchText")]
        //public IdNameReadonlyDto Department { get; set; }
        /*public int DepartmentId { get; set; }
        public IList<IdNameDto> Departments;*/


        [Display(Name = "Должность")]
        public int PositionId { get; set; }

        /*[Display(Name = "Вид отпуска")]
        public int VacationTypeId { get; set; }
        public IList<IdNameDto> VacationTypes;*/

        [Display(Name = "Период с")]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int RequestStatusId { get; set; }

        //[Display(Name = "Документы")]
        public IList<VacationDto> Documents { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }
    }
}