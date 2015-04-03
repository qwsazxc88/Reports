﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintAgreePersonForCheckingModel : AbstractEmploymentModel
    {
        public DateTime? EmploymentDate { get; set; }
        public string EmployeeName { get; set; }
    }
}
