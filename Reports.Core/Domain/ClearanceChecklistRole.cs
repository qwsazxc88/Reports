using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Расширенные роли пользователей в дополнение к заданным через RoleId
    /// </summary>
    public class ClearanceChecklistRole : AbstractEntityWithVersion
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        // Срок (в днях относительно даты увольнения), в течение которого доступно согласование для данной роли
        public virtual int? DaysForApproval { get; set; }
        // Дата удаления роли
        public virtual DateTime? DeleteDate { get; set; }
    }
}
