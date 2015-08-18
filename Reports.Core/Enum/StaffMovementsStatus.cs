using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Enum
{
    public enum StaffMovementsStatus
    {
        //Черновик
        Temp =0,
        //Отправлена на согласование руководителю отпускающему
        SourceManager =1,
        //Отправлена на согласование руководителю принимающему
        TargetManager =2,
        //Отправлена на согласование кадровику банка
        PersonelManagerBank =3,
        //Заблокирована кадровиком банка
        Blocked = 4,
        //Отправлена на согласование вышестоящему руководителю
        Chief =5,
        //Оформление кадры
        Personnel =6,
        //Контроль руководителя - пакет документов на подпись
        ChiefControl=7,
        //Документы подписаны
        DocsApproved = 8,
        //Перевод оформлен
        Approved = 9,
        //Отклонен
        Canceled =10,
        //Выгружена в 1С
        SendTo1c =11
    }
}
