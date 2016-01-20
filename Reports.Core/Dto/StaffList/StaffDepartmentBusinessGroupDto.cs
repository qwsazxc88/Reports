using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class StaffDepartmentBusinessGroupDto
    {
        public int bId { get; set; }
        public string bCode { get; set; }
        public string bName { get; set; }
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public int bDepartmentId { get; set; }
        public string DepName { get; set; }
        public string ManagementName { get; set; }
        public string BranchName { get; set; }
        public string DepManager { get; set; }
    }
}
