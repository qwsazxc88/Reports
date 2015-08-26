using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Enum
{
    public enum StaffMovementsRejectReasonEnum
    {
       /// <summary>
       /// 1 Отказ сотрудника
       /// </summary> 
        UserReject = 1,
	    
        /// <summary>
        /// 2 Отклонена отпускающим руководителем
        /// </summary> 
        SourceManagerReject = 2,

        /// <summary>
        /// 3 Отклонена принимающим руководителем
        /// </summary> 
        TargetManagerReject = 3,
	    
        /// <summary>
        /// 4 Отклонена вышестоящим руководителем
        /// </summary>
        ChiefManagerReject = 4,

        /// <summary>
        /// 5 Отклонена кадровиком банка
        /// </summary>
        PersonnelManagerBankReject = 5,

        /// <summary>
        /// 6 Отклонена кадровиком
        /// </summary>
        PersonnelManagerReject = 6,
    }
}
