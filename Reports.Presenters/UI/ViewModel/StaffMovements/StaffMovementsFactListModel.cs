using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class StaffMovementsFactListModel
    {
        [SearchField(Comparer = ComparerEnum.Like, ModelParam = "User.Name")]
        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }
        [SearchField(Comparer = ComparerEnum.Equals, ModelParam = "StaffMovemnts.Id", IsNullable = true)]
        [Display(Name = "Номер заявки кадрового перемещения")]
        public int? StaffMovementsId { get; set; }
        [SearchField(Comparer = ComparerEnum.Equals, ModelParam = "StaffEstablishedPostRequest.Id", IsNullable = true)]
        [Display(Name = "Номер заявки на изменение ШЕ")]
        public int? StaffEstablishedPostRequestId { get; set; }
        [SearchField(Comparer = ComparerEnum.Equals, ModelParam = "SendTo1C")]
        [Display(Name = "Дата перемещения")]
        public DateTime? MovemntDate { get; set; }
        [Display(Name = "Подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
    }
}
