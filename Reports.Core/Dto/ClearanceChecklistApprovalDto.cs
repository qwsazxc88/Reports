using System;

namespace Reports.Core.Dto
{
    public class ClearanceChecklistApprovalDto
    {
        // ID
        public int Id { get; set; }
        // Согласующее подразделение
        public string ClearanceChecklistDepartment { get; set; }
        // Кем согласовано
        public string ApprovedBy { get; set; }
        // Дата согласования
        public string ApprovalDate { get; set; }
        // Доступность для согласования текущим пользователем
        public bool Active { get; set; }
    }
}