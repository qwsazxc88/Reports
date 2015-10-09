using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class StaffDepartmentBusinessGroupDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public int DepartmentId { get; set; }
        public string DepName { get; set; }
    }
}
