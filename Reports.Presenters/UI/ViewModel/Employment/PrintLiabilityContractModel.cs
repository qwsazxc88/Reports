﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintLiabilityContractModel : AbstractEmploymentModel
    {
        public DateTime ContractDate { get; set; }
        public string EmployerRepresentativeNameShortened { get; set; }
        public string EmployerRepresentativePosition { get; set; }
        public string EmployerRepresentativeTemplate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNameShortened { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeePassportSeriesNumber { get; set; }
        public DateTime? EmployeePassportDateOfIssue { get; set; }
        public string EmployeePassportIssuedBy { get; set; }
        
        public PrintLiabilityContractModel()
        {

        }
    }
}