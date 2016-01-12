using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class StaffDepartmentAdministrationDto
    {
        public int aId { get; set; }
        public string aCode { get; set; }
        public string aName { get; set; }
        public int ManagementId { get; set; }
        public string ManagementName { get; set; }
        public int aDepartmentId { get; set; }
        public string DepName { get; set; }
        public string BranchName { get; set; }
        public string DepManager { get; set; }
    }
}
