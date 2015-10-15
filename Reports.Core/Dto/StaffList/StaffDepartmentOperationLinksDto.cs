using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class StaffDepartmentOperationLinksDto
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public bool IsLink { get; set; }
    }
}
