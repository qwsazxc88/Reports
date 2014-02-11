using System;

namespace Reports.Core.Dto
{
    public class ClearanceChecklistApprovalDto
    {
        public string ClearanceChecklistDepartment { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
