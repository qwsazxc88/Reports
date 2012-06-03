using System;

namespace Reports.Core.Dto
{
    public class ImportDto
    {
        public string ManagerName { get; set; }
        public string ManagerCode { get; set; }
        public string PersonnelName { get; set; }
        public string PersonnelCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime? DateAccept { get; set; }
        public DateTime? DateRelease { get; set; }
        public string Login { get; set; }
        public string Comment { get; set; }
        public UserRole Role { get; set; }
    }
}