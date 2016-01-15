using System;

namespace Reports.Core.Enum
{
    public enum ApprovalTypeEnum
    {
        /// <summary>
        /// Заявки штатного расписания для подразделений.
        /// </summary>
        StaffDepartmentRequest = 1,
        /// <summary>
        /// Заявки штатного расписания для штатных единиц.
        /// </summary>
        StaffEstablishedPostRequest = 2,
        /// <summary>
        /// Кадровые перемещения
        /// </summary>
        StaffMovements = 3
        
    }
}
