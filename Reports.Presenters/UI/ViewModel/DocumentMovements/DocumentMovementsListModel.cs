using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Dto;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DocumentMovementsListModel
    {
        public DocumentMovementsListModel()
        {
            Statuses = new List<IdNameDto>();
            Statuses.Add(new IdNameDto { Id=0, Name="Все" });
            Statuses.Add(new IdNameDto { Id = 1, Name = "Черновик" });
            Statuses.Add(new IdNameDto { Id = 2, Name = "Отправлено" });
            Statuses.Add(new IdNameDto { Id = 3, Name = "Получено" });
        }
        public bool IsSaveAvailable { get; set; }
        public bool IsAddAvailable { get; set; }
        public List<IdNameDto> DocTypes { get; set; }
        [Display(Name="Тип документа")]
        
        public int DocType { get; set; }
        public IList<IdNameDto> Statuses { get; set; }
        [SearchField(Comparer = ComparerEnum.Equals, ModelParam = "StatusId", IgnoreValue = 0)]
        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        [SearchField(Comparer = ComparerEnum.Like, ModelParam = "User.Name")]
        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }
        [SearchField(Comparer=ComparerEnum.Like,ModelParam="Sender.Name")]
        [Display(Name="ФИО отправителя")]
        public string SenderName { get; set; }
        [SearchField(Comparer = ComparerEnum.Like, ModelParam = "Receiver.Name")]
        [Display(Name = "ФИО получателя")]
        public string ReceiverName { get; set; }
        [SearchField(Comparer = ComparerEnum.Equals, ModelParam = "Id",IsNullable=true)]
        [Display(Name = "Номер заявки")]
        public int? Number { get; set; }

        [SearchField(Comparer = ComparerEnum.EqualsOrGreatar, ModelParam = "SendDate")]
        [Display(Name = "С")]
        public DateTime? BeginDate { get; set; }
        [SearchField(Comparer = ComparerEnum.EqualsOrLess, ModelParam = "SendDate")]
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }
        [SearchField(Comparer = ComparerEnum.Department, ModelParam = "User.Department", IgnoreValue = 0)]
        [Display(Name = "Подразделение")]
        public int DepartmentId { get; set; }
        [Display(Name = "Подразделение")]
        public string DepartmentName { get; set; }        
    }
}
