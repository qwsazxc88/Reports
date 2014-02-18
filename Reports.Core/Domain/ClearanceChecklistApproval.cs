﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class ClearanceChecklistApproval : AbstractEntityWithVersion
    {
        // Родительский обходной лист
        public virtual ClearanceChecklist ClearanceChecklist { get; set; }

        // Согласующее подразделение
        public virtual ClearanceChecklistDepartment ClearanceChecklistDepartment { get; set; }
        // Расширенная роль для согласования
        public virtual ExtendedRole ExtendedRole { get; set; }

        // Согласующий; заполняется по факту согласования
        public virtual User ApprovedBy { get; set; }
        public virtual DateTime? ApprovalDate { get; set; }
    }
}
