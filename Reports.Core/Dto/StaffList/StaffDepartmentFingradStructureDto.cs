using System;
using System.Collections.Generic;


namespace Reports.Core.Dto
{
    public class StaffDepartmentFingradStructureDto
    {
        public int Id { get; set; }
        public string RPLinkCode { get; set; }
        public string RPLinkName { get; set; }
        public string RPLinkNameSKD { get; set; }
        public string BGCode { get; set; }
        public string BGName { get; set; }
        public string BGNameSKD { get; set; }
        public string AdminCode { get; set; }
        public string AdminName { get; set; }
        public string AdminNameSKD { get; set; }
        public string ManagementCode { get; set; }
        public string ManagementName { get; set; }
        public string ManagementNameSKD { get; set; }
    }
}
