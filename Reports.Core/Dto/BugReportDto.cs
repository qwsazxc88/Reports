using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class BugReportDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserRole { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}
