using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    class ClearanceChecklistEditModel
    {
        public IList<ClearanceChecklistApprovalDto> ClearanceChecklistApprovals { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
