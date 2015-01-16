using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintPersonalDataObligationModel : AbstractEmploymentModel
    {        
        public string EmployeeName { get; set; }
        public string EmployeeNameShortened { get; set; }

        public PrintPersonalDataObligationModel()
        {

        }
    }
}