using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintInstructionOfSecretModel : AbstractEmploymentModel
    {
        public DateTime? EmploymentDate { get; set; }
        public string EmployeeName { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
    }
}
