using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core;
namespace Reports.Core.Dto
{
    public class DocumentMovementsDto
    {     
        [ngGrid(ColumnName="Номер", FilterEnabled = true, IsEditable= false, SortEnabled = true, Type = ngType.Number)]
        public int Id { get; set; }
        [ngGrid(ColumnName = "ФИО сотрудника", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string User { get; set; }
        [ngGrid(ColumnName = "Дирекция 3 ур.", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string UserDep3 { get; set; }
        [ngGrid(ColumnName = "Подразделение 7 ур.", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string UserDep7 { get; set; }
        [ngGrid(ColumnName = "Направление", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string Direction { get; set; }
        [ngGrid(ColumnName = "Отправитель", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string Sender { get; set; }
        [ngGrid(ColumnName = "Получатель", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string Receiver { get; set; }
        [ngGrid(ColumnName = "Дата создания", FilterEnabled = true, Type = ngType.Date, IsEditable = false, SortEnabled = true)]
        public DateTime CreateDate { get; set; }
        [ngGrid(ColumnName = "Дата отправки", FilterEnabled = true, Type = ngType.Date, IsEditable = false, SortEnabled = true)]
        public DateTime? SendDate { get; set; }
        [ngGrid(ColumnName = "Описание", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string Descript { get; set; }
        [ngGrid(ColumnName = "Наименование документа", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string DocumentName { get; set; }
        [ngGrid(ColumnName = "Документ отправлен", FilterEnabled = true, Type = ngType.Boolean, IsEditable = false, SortEnabled = true)]
        public bool DocumentSended { get; set; }
        [ngGrid(ColumnName = "Документ получен", FilterEnabled = true, Type = ngType.Boolean, IsEditable = false, SortEnabled = true)]
        public bool DocumentReceived { get; set; }
        [ngGrid(ColumnName = "Статус", FilterEnabled = true, Type = ngType.String, IsEditable = false, SortEnabled = true)]
        public string Status { get; set; }
        
    }
}
