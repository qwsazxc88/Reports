using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Enum
{
    public enum StaffMovementsStatus
    {
        /// <summary>
        /// 1 Черновик
        /// </summary>
        Temp =1,

        /// <summary>
        /// 2 Отправлена на согласование руководителю отпускающему
        /// </summary>
        SourceManager =2,

        /// <summary>
        /// 3 Отправлена на согласование руководителю принимающему
        /// </summary>
        TargetManager =3,

        /// <summary>
        /// 4 Отправлена на согласование кадровику банка
        /// </summary>
        PersonelManagerBank =4,
        
        /// <summary>
        /// 5 Заблокирована кадровиком банка
        /// </summary>
        Blocked = 5,

        /// <summary>
        /// 6 Отправлена на согласование вышестоящему руководителю
        /// </summary>
        Chief =6,

        /// <summary>
        /// 7 Оформление кадры
        /// </summary>
        Personnel =7,

        /// <summary>
        /// 8 Контроль руководителя - пакет документов на подпись
        /// </summary>
        ChiefControl=8,

        /// <summary>
        /// 9 Документы подписаны
        /// </summary>
        DocsApproved = 9,

        /// <summary>
        /// 10 Перевод оформлен
        /// </summary>
        Approved = 10,

        /// <summary>
        /// 11 Отклонен
        /// </summary>
        Canceled =11,

        /// <summary>
        /// 12 Выгружена в 1С
        /// </summary>
        SendTo1c =12
    }
}
