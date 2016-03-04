using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class AccessGroupListDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PositionName { get; set; }
        public string AccessGroupCode { get; set; }
        public string AccessGroupName { get; set; }
        public string Dep7Name { get; set; }
        public string Dep3Name { get; set; }
        public string Email { get; set; }
        public string AlternativeMail { get; set; }
        public string Phone { get; set; }
        public DateTime? EndDate { get; set; }
        public string Manager6 { get; set; }
        public string Manager5 { get; set; }
        public string Manager4 { get; set; }
        public decimal SaldoPrimary{ get; set; }
        public decimal SaldoAdditional { get; set; }
        public DateTime? DateAccept { get; set; }
    }
}
