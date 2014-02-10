using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionUserDeptsListModel : BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }


        [Display(Name = "Статус приказа")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        //public bool IsAddAvailable { get; set; }
        //public bool IsApproveAvailable { get; set; }
        //public bool IsApproveClick { get; set; }

        public IList<MissionUserDeptsListDto> Documents { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public bool HasErrors { get; set; }
        public bool IsPrintAvailable { get; set; }

        #region BeginEndCreateDate Members

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        #endregion
    }
    public class PrintMissionUserDeptsListModel
    {
        public IList<MissionUserDeptsListDto> Documents { get; set; }
    }
}