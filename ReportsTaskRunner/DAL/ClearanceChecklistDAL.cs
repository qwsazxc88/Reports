using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsTaskRunner.DAL
{
    class ClearanceChecklistDAL
    {
        public static IList<User> GetClearanceChecklistApprovingAuthorities()
        {
            return MainDAL.db.User
                // К которым привязаны службы ОЛ
                .Where<User>(u => u.ClearanceChecklistRoleRecords != null
                    // и среди этих служб есть активные
                    && u.ClearanceChecklistRoleRecords.Where<ClearanceChecklistRoleRecords>(ccrr => ccrr.ClearanceChecklistRole.DeleteDate == null).Count() > 0)
                .ToList<User>();
        }

        public static IList<Dismissal> GetClearanceChecklistsByDismissalDate(DateTime dismissalDate)
        {
            IList<Dismissal> ccls = MainDAL.db.Dismissal
                .Where<Dismissal>(ccl => ccl.EndDate == dismissalDate
                    // Где есть незавершенные согласования
                    && ccl.ClearanceChecklistApproval./*Where(ccla => ccla.ApprovalDate != null).*/Count() > 0)
                .ToList<Dismissal>();
            return ccls;
        }
    }
}
