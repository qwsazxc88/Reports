using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI;
using Reports.Core.Dto;
using Reports.Core;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    [SearchModel(RightsProperty="User.Department")]
    public class VacationReturnListModel
    {
        public IList<IdNameDto> Statuses { get; set; }
        [SearchField( Comparer=ComparerEnum.Equals, ModelParam="Status.Id", IgnoreValue=0)]
        [Display(Name="Статус")]
        public int StatusId { get; set; }
        [SearchField(Comparer = ComparerEnum.EqualsOrGreatar, ModelParam = "ReturnDate")]
        [Display(Name = "Период с")]
        public DateTime? BeginDate { get; set; }
        [SearchField(Comparer = ComparerEnum.EqualsOrLess, ModelParam = "ContinueDate")]
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }
        [SearchField(Comparer = ComparerEnum.Department, ModelParam = "User.Department", IgnoreValue=0)]
        [Display(Name = "Подразделение")]
        public int DepartmentId { get; set; }
        [Display(Name = "Подразделение")]
        public string DepartmentName { get; set; }
        [SearchField(Comparer = ComparerEnum.Like, ModelParam = "User.Name")]
        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }
        [SearchField(Comparer=ComparerEnum.Equals,ModelParam="Id", IgnoreValue=0, IsNullable=true)]
        [Display(Name = "Номер заявки")]
        public int? Number { get; set; }
        public bool IsCreateAvailable { get; set; }
    }
}
