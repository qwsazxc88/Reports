using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class StaffMovementsListModel
    {
        [Display(Name="ФИО сотрудника")]
        public string UserName { get; set; }
        [Display(Name="Номер заявки")]
        public int? Number { get; set; }
        [Display(Name="Подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        [Display(Name="Статус")]
        public int Status { get; set; }
        public IList<IdNameDto> Statuses { get; set; }
        [Display(Name = "Вид заявки")]
        public int TypeId { get; set; }
        public IList<IdNameDto> Types { get; set; }
        [Display(Name="Период с")]
        public DateTime? BeginDate { get; set; }
        [Display(Name="По")]
        public DateTime? EndDate { get; set; }
    }
}
