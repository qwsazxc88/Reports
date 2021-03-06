﻿using System;
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
        [ngGridRow(field = "Id", displayName = "Номер", enableFiltering = true, enableSorting = true, type = "number",
            cellTemplate = "<div class='ui-grid-cell-contents'><a target='_blank' href='/StaffMovements/StaffMovementsFactEdit/{{row.entity[col.field]}}'>{{row.entity[col.field]}}</a> </div>")]
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
            cellTemplate = "<div class='ui-grid-cell-contents'><a target='_blank' ng-show='row.entity[col.field]!=0' href='/StaffList/StaffDepartmentRequest/?id={{row.entity[col.field]}}&RequestType=0&DepartmentId=0'>{{row.entity[col.field]}}</a> </div>")]
        public int StaffEstablishedPostRequestId { get; set; }
        [ngGridRow(field = "StaffMovementsId",
            displayName = "Номер кадрового перемещения",
            enableFiltering = true,
            enableSorting = true,
            type = "number",
            cellTemplate = "<div class='ui-grid-cell-contents'><a ng-show='row.entity[col.field]!=0' target='_blank' href='/StaffMovements/Edit/{{row.entity[col.field]}}'>{{row.entity[col.field]}}</a> </div>")]
        public int StaffMovementsId { get; set; }
        [ngGridRow(field = "IsOk",
            displayName = "Состояние выгрузки",
            enableFiltering = true,
            enableSorting = true,
            cellTemplate = "<div class='ui-grid-cell-contents' ng-class='{bgRed: row.entity[col.field]!=true, bgGreen: row.entity[col.field]==true}'><span class='bgRed' ng-show='row.entity[col.field]!=true'>Не проведено</span><span class='bgGreen'  ng-show='row.entity[col.field]==true'>Проведено</span></div>")]
        public bool IsOk { get; set; }
    }
}
