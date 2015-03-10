using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class AnalyticalStatementDetailsDto
    {
        public int  OrderNumber {get;set;}
	    public DateTime OrderDate {get;set;}
	    public float Ordered {get;set;}
	   	public int? ReportNumber {get;set;}
	    public DateTime? ReportDate {get;set;}
	    public float? ReportedSum {get;set;}
        public DateTime? SendTo1C { get; set; }
        public float SaldoStart { get; set; }
    }
}
