using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Reports.Core.Dto
{
    public class AnalyticalStatementDetailsDto
    {
        public int  OrderNumber {get;set;}
	    public DateTime OrderDate {get;set;}
        [DisplayFormat(DataFormatString = "{0.00}")]
	    public float Ordered {get;set;}
	   	public int? ReportNumber {get;set;}
	    public DateTime? ReportDate {get;set;}
        [DisplayFormat(DataFormatString = "{0.00}")]
	    public float? ReportedSum {get;set;}
        public DateTime? SendTo1C { get; set; }
        [DisplayFormat(DataFormatString = "{0.00}")]
        public float SaldoStart { get; set; }
    }
}
