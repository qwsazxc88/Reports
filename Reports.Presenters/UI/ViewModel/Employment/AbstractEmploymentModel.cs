using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class AbstractEmploymentModel
    {
        public int Version { get; set; }
        public int UserId { get; set; }
    }
}
