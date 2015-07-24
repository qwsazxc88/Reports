using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class ManualDeductionDto
    {
        public int UserId {get;set;}
        public string UserName { get; set; }
        public decimal AllSum { get; set; }
        public DateTime DeductionDate { get; set; }
        public DateTime? SendTo1C { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
