using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HolidayWorkListModel :BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        [AutoComplete("AutoComplete", "Departments", "searchText")]
        public IdNameReadonlyDto Department { get; set; }
        //public int DepartmentId { get; set; }
        //public IList<IdNameDto> Departments;
        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;
        [Display(Name = "Вид оплаты/доплаты")]
        public int TypeId { get; set; }
        public IList<IdNameDto> Types;
        public string ManagerName { get; set; }
        [Display(Name = "Дата создания заявки с")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }
        //[Display(Name = "Процент оплаты заработка")]
        //public int PaymentPercentType { get; set; }
        //public IList<IdNameDtoSort> PaymentPercentTypes;

        //[Display(Name = "Дата начала")]
        //public DateTime? BeginDate { get; set; }
        //[Display(Name = "Дата окончания")]
        //public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        //[Display(Name = "Документы")]
        public IList<VacationDto> Documents { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }
        [Display(Name="Номер заявки")]
        public string Number { get; set; }


    }

    
}