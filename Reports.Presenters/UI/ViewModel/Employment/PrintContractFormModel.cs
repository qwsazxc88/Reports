using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintContractFormModel : AbstractEmploymentModel
    {
        public string EmployerRepresentativeTemplate { get; set; }
        public string EmployerRepresentativeNameShortened { get; set; }
        public string Number { get; set; }
        // public string City { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNameShortened { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public bool IsSecondaryJob { get; set; }
        public string WorkCity { get; set; }
        public string ProbationaryPeriod { get; set; }
        public decimal? SalaryBasis { get; set; }
        public string Schedule { get; set; }
        // Реквизиты подразделения
        public string PassportSeriesNumber { get; set; }
        public string PassportIssuedBy { get; set; }
        public DateTime? PassportDateOfIssue { get; set; }
        public string EmployeeAddress { get; set; }
        public bool? IsFixedTermContract { get; set; }

        public PrintContractFormModel()
        {            
        }
    }
}
