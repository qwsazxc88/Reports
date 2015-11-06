using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class DocumentMovementsListModel
    {
        [SearchField(Comparer = ComparerEnum.Department, ModelParam = "User.Department", IgnoreValue = 0)]
        [Display(Name = "Подразделение")]
        public int DepartmentId { get; set; }
        [SearchField(Comparer = ComparerEnum.Like, ModelParam = "User.Name")]
        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }
        [SearchField(Comparer=ComparerEnum.Like,ModelParam="Sender.Name")]
        [Display(Name="ФИО отправителя")]
        public string SenderName { get; set; }
        [SearchField(Comparer = ComparerEnum.Like, ModelParam = "Receiver.Name")]
        [Display(Name = "ФИО отправителя")]
        public string ReceiverName { get; set; }
        [SearchField(Comparer = ComparerEnum.Equals, ModelParam = "Id")]
        [Display(Name = "Номер заявки")]
        public int Number { get; set; }
        public IList<DocumentMovementsDto> Documents { get; set; }
    }
}
