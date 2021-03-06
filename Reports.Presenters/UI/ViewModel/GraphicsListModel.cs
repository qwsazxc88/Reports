﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class GraphicsListModel : IDepartmentSelect, IYearMonthSelection
    {
        //public int ManagerId { get; set; }

        [Display(Name = "Месяц")]
        public int Month { get; set; }
        public IList<IdNameDto> Monthes { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }
        public IList<IdNameDto> Years { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        public bool IsSetShortNamesAvailable { get; set; }
        public bool IsShortNamesEditable { get; set; }

        [DataType("TerraGraphicDtoList")]
        public IList<TerraGraphicDto> Dtos { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public bool ForPrint { get; set; }
    }
}