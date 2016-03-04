using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class SicklistListModel :BeginEndCreateDate
    {
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
        public IList<IdNameDto> Positions;
        [Display(Name = "Вид больничного")]
        public int TypeId { get; set; }
        public IList<IdNameDto> Types;

        //[Display(Name = "Процент оплаты заработка")]
        //public int PaymentPercentType { get; set; }
        //public IList<IdNameDtoSort> PaymentPercentTypes;

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public BeginEnd Receive { get; set; }
        public BeginEnd Send { get; set; }
        public BeginEnd Filled { get; set; }

        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        //[Display(Name = "Документы")]
        public IList<SicklistDto> Documents { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Номер больничного листа")]
        public string SicklistNumber { get; set; }
        [Display(Name="Номер заявки")]
        public string Number { get; set; }
        public bool IsOriginalReceivedModified { get; set; }
        public bool IsOriginalReceivedEditable { get; set; }

    }

    
}