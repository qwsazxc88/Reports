using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Reports.Core.Dto
{
    public class AnalyticalStatementDetailsDto
    {
        public int  Number {get;set;}
	    public DateTime Date {get;set;}
        [DisplayFormat(DataFormatString = "{0.00}")]
	    public float Ordered {get;set;}
	   	[DisplayFormat(DataFormatString = "{0.00}")]
	    public float Reported {get;set;}
        public DateTime? SendTo1C { get; set; }
        [DisplayFormat(DataFormatString = "{0.00}")]
        public float SaldoStart { get; set; }
        [DisplayFormat(DataFormatString = "{0.00}")]
        public float SaldoEnd { get; set; }
        public int DocType { get; set; }
        public int DocId { get; set; }
    }
}
