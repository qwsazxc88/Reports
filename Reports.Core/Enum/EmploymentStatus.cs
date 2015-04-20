namespace Reports.Core.Enum
{
    public enum EmploymentStatus
    {
        PENDING_APPROVAL_BY_SECURITY = 1,//Ожидает согласование ДБ
        PENDING_REPORT_BY_TRAINER = 2,//Обучение
        PENDING_APPLICATION_LETTER = 3,//Ожидается заявление о приеме
        PENDING_APPROVAL_BY_MANAGER = 4,//Ожидает согласование руководителем
        PENDING_APPROVAL_BY_HIGHER_MANAGER = 5,//Ожидает согласование вышестоящим руководителем
        //
        PENDING_FINALIZATION_BY_PERSONNEL_MANAGER = 6,//Оформление Кадры
        COMPLETE = 7,//Принят
        SENT_TO_1C = 8,//Выгружено в 1С
        REJECTED = 9 //Отклонен
    }
}