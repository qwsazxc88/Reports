using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class StaffDepartmentManagementDto
    {
        public int mId { get; set; }
        public string mCode { get; set; }
        public string mName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int mDepartmentId { get; set; }
        public string DepName { get; set; }
    }
}
