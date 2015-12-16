using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class StaffMovementsFactDto
    {
        public StaffMovementsFactDto()
        {           
        }
        [ngGridRow(field = "Id", displayName = "Номер", enableFiltering = true, enableSorting = true, type = "number")]
        public int Id { get; set; }
        [ngGridRow(field = "SendTo1C", displayName = "Дата", enableFiltering = true, enableSorting = true, type = "date", cellTemplate = "<div class='ui-grid-cell-contents'>{{row.entity[col.field]| date: 'dd.MM.yyyy'}}</div>")]
        public DateTime SendTo1C { get; set; }
        [ngGridRow(field = "User", displayName = "ФИО сотрудника", enableCellEdit = true, enableFiltering = true, enableSorting = true, type = "string")]
        public string User { get; set; }
        [ngGridRow(field = "UserDep3", displayName = "Дирекция 3 ур.", enableFiltering = true, enableSorting = true, type = "string")]
        public string UserDep3 { get; set; }
        [ngGridRow(field = "UserDep7", displayName = "Подразделение 7 ур.", enableFiltering = true, enableSorting = true, type = "string")]
        public string UserDep7 { get; set; }
        [ngGridRow(field = "StaffEstablishedPostRequestId", 
            displayName = "Номер заявки на изменение ШЕ", 
            enableFiltering = true, 
            enableSorting = true, 
            type = "number", 
            cellTemplate = "<div class='ui-grid-cell-contents'><a target='_blank' href='/StaffList/DocumentMovementsEdit/{{row.entity[col.field]}}'>{{row.entity[col.field]}}</a> </div>")]
        public int StaffEstablishedPostRequestId { get; set; }
        [ngGridRow(field = "StaffEstablishedPostRequestId",
            displayName = "Номер кадрового перемещения",
            enableFiltering = true,
            enableSorting = true,
            type = "number",
            cellTemplate = "<div class='ui-grid-cell-contents'><a target='_blank' href='/StaffMovements/Edit/{{row.entity[col.field]}}'>{{row.entity[col.field]}}</a> </div>")]
        public int StaffMovementsId { get; set; }
    }
}
