using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class ClearanceChecklistApprovalDto
    {
        // ID
        public int Id { get; set; }
        // Согласующее подразделение
        public string ClearanceChecklistRole { get; set; }
        // Список лиц, имеющих право согласования от имени подразделения
        public IList<string> RoleAuthorities { get; set; }
        // Кем согласовано
        public string ApprovedBy { get; set; }
        // Дата согласования
        public string ApprovalDate { get; set; }
        // Комментарий согласующего
        public string Comment { get; set; }
        // Доступность для согласования текущим пользователем
        public bool Active { get; set; }
    }
}