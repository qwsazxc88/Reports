using System;
using System.Collections.Generic;


namespace Reports.Core.Dto
{
    public class StaffDepartmentBranchDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepName { get; set; }
    }
}
