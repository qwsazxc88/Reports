using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Enum
{
    public enum StaffMovementsDocsTypes
    {
        /// <summary>
        /// Печать яаявления о переводе
        /// </summary>
        MovementNote=1,
        /// <summary>
        ///Печать дополнительного соглашения
        /// </summary>
        AdditionalAgreementDoc=2,
        /// <summary>
        ///Печать приказа о переводе
        /// </summary>
        MovementOrderDoc=3,
        /// <summary>
        ///Печать договора Мат.ответственность
        /// </summary>
        MaterialLiabilityDoc=4,
        /// <summary>
        ///Печать порядок по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)
        /// </summary>
        RequirementsOrderDoc=5,
        /// <summary>
        ///Печать порядок по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)
        /// </summary>
        ServiceOrderDoc =6
    }
}
