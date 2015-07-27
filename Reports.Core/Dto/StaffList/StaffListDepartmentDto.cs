using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Подразделения с количеством штатных единиц
    /// </summary>
    public class StaffListDepartmentDto : Department
    {
        public int SEPCount { get; set; }
        public string DepFingradName { get; set; }
    }
}
