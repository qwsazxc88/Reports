using System;

namespace Reports.Core.Dto
{
    public class MissionUserDeptsListDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string UserName { get; set; }
        public string ReportNumber { get; set; }
        public DateTime ReportDate { get; set; }
        public decimal DiffSum { get; set; }
        public decimal AccountantSum { get; set; }
        public decimal UserSum { get; set; }
        public string Status { get; set; }
    }
}