using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Enum
{
    public enum CommentsPlaceTypes
    {
        /// <summary>
        /// Начисления
        /// </summary>
        SurchargeNote =1,
        /// <summary>
        /// Заявки штатного расписания для подразделения
        /// </summary>
        StaffListDepratment = 2,
        /// <summary>
        /// Кадровые перемещения
        /// </summary>
        StaffMovements =3,
        /// <summary>
        /// Заявки штатного расписания для штатных единиц.
        /// </summary>
        StaffListEstablishedPost = 4
    }
}
