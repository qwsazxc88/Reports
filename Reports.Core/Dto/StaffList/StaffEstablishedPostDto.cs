using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Для списка штатных единиц в пределах подразделения.
    /// </summary>
    public class StaffEstablishedPostDto
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int DepartmentId { get; set; }
        public int Quantity { get; set; }
        public decimal? Salary { get; set; }
        public decimal? StaffECSalary { get; set; }
        public string Path { get; set; }    //поле из подразделения, используется в дереве
        public int RequestId { get; set; }  //Id действующей заявки
    }
}
