using System;

namespace Reports.Core.Dto
{
    public class ClearanceChecklistApprovalDto
    {
        // Согласующее подразделение
        public string ClearanceChecklistDepartment { get; set; }
        // Кем согласовано
        public string ApprovedBy { get; set; }
        // Дата согласования
        public DateTime? ApprovalDate { get; set; }
        // Доступность для согласования текущим пользователем
        public bool Active { get; set; }
    }
}