using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class SurchargeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; }
        public DateTime EditDate { get; set; }
        public string Dep3Name { get; set; }
        public decimal Sum { get; set; }
        public DateTime SurchargeDate { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string Dep7Name { get; set; }
        public string Status { get; set; }
        public int MissionReportNumber { get; set; }
    }
}
