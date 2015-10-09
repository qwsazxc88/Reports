using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class StaffDepartmentRPLinkDto
    {
        public int rId { get; set; }
        public string rCode { get; set; }
        public string rName { get; set; }
        public int BGId { get; set; }
        public string BGName { get; set; }
        public int rDepartmentId { get; set; }
        public string DepName { get; set; }
    }
}
