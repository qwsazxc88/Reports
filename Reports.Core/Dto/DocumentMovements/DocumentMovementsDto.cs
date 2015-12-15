using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core;
namespace Reports.Core.Dto
{
    public class DocDto
    {
        public int DocType { get; set; }
        [ngGridRow(field = "DocumentName", displayName = "Наименование")]
        public string DocumentName { get; set; }
        [ngGridRow(field = "DocumentSended", displayName = "Отправлено", cellTemplate = "<input type='checkbox' ng-model='row.entity[col.field]' disabled='disabled'/>")]
        public bool DocumentSended { get; set; }
        [ngGridRow(field = "DocumentReceived", displayName = "Получено", enableCellEdit=true, cellTemplate = "<input type='checkbox' ng-model='row.entity[col.field]' disabled='disabled' />", cellEditTemplate = "<input type='checkbox' ng-model='row.entity[col.field]' ng-disable='!grid.appScope.IsGridEditable'/>")]
        public bool DocumentReceived { get; set; }
        public DocDto()
        {                        
            DocumentName = "";
            DocumentSended = false;
            DocumentReceived = false;
        }
    }
    public class DocumentMovementsDto
    {
        public DocumentMovementsDto()
        {
            Id = 0;
            User = "";
            UserDep3 = "";
            UserDep7 = "";
            Direction = "";
            Sender = "";
            Receiver = "";
            CreateDate = new DateTime();
            SendDate = new DateTime?();
            ReceiveDate = new DateTime?();
            Descript = "";
            Status = "";
        }
        [ngGridRow(field = "Id", displayName = "Номер", enableFiltering = true, enableSorting = true, type = "number", cellTemplate = "<div class='ui-grid-cell-contents'><i ng-class=\"{ 'ui-grid-icon-plus-squared' : !row.isExpanded, 'ui-grid-icon-minus-squared' : row.isExpanded }\" ng-click='grid.appScope.toggleRow(row.entity)'></i><a target='_blank' href='/DocumentMovements/DocumentMovementsEdit/{{row.entity[col.field]}}'>{{row.entity[col.field]}}</a> </div>")]
        public int Id { get; set; }
        [ngGridRow(field = "User", displayName = "ФИО сотрудника", enableCellEdit = true, enableFiltering = true, enableSorting = true, type = "string")]
        public string User { get; set; }
        [ngGridRow(field = "UserDep3", displayName = "Дирекция 3 ур.", enableFiltering = true, enableSorting = true, type = "string")]
        public string UserDep3 { get; set; }
        [ngGridRow(field = "UserDep7", displayName = "Подразделение 7 ур.", enableFiltering = true, enableSorting = true, type = "string")]
        public string UserDep7 { get; set; }
        [ngGridRow(field = "Direction", displayName = "Направление", enableFiltering = true, enableSorting = true, type = "string")]
        public string Direction { get; set; }
        [ngGridRow(field = "Sender", displayName = "Отправитель", enableFiltering = true, enableSorting = true, type = "string")]
        public string Sender { get; set; }
        [ngGridRow(field = "Receiver", displayName = "Получатель", enableFiltering = true, enableSorting = true, type = "string")]
        public string Receiver { get; set; }
        [ngGridRow(field = "CreateDate", displayName = "Дата создания", enableFiltering = true, enableSorting = true, type = "date", cellTemplate = "<div class='ui-grid-cell-contents'>{{row.entity[col.field]| date: 'dd.MM.yyyy'}}</div>")]
        public DateTime CreateDate { get; set; }
        [ngGridRow(field = "SendDate", displayName = "Дата отправки", enableFiltering = true, enableSorting = true, type = "date", cellTemplate = "<div class='ui-grid-cell-contents'>{{row.entity[col.field]| date: 'dd.MM.yyyy'}}</div>")]
        public DateTime? SendDate { get; set; }
        [ngGridRow(field = "ReceiveDate", displayName = "Дата получения", enableFiltering = true, enableSorting = true, type = "string")]
        public DateTime? ReceiveDate { get; set; }
        [ngGridRow(field = "Descript", displayName = "Описание", enableFiltering = true, enableSorting = true, type = "string")]
        public string Descript { get; set; }
        [ngGridRow(field = "Status", displayName = "Статус", enableFiltering = true, enableSorting = true, type = "string")]
        public string Status { get; set; }
        public object subGridOptions { get; set; }
        public bool ReceiverAccept { get; set; }
    }
}
