using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintEmploymentOrderModel : AbstractEmploymentModel
    {
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Conditions { get; set; }
        // тарифная ставка
        // надбавка
        public string ProbationaryPeriod { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        
        public PrintEmploymentOrderModel()
        {            
        }
    }
}
