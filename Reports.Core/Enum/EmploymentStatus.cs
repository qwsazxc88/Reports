namespace Reports.Core.Enum
{
    public enum EmploymentStatus
    {
        /// <summary>
        /// Ожидает согласование ДБ
        /// </summary>
        PENDING_APPROVAL_BY_SECURITY = 1,
        /// <summary>
        /// Обучение
        /// </summary>
        PENDING_REPORT_BY_TRAINER = 2,//Обучение
        /// <summary>
        /// Ожидается заявление о приеме
        /// </summary>
        PENDING_APPLICATION_LETTER = 3,//Ожидается заявление о приеме
        /// <summary>
        /// Ожидает согласование руководителем
        /// </summary>
        PENDING_APPROVAL_BY_MANAGER = 4,//Ожидает согласование руководителем
        /// <summary>
        /// Ожидает согласование вышестоящим руководителем
        /// </summary>
        PENDING_APPROVAL_BY_HIGHER_MANAGER = 5,//Ожидает согласование вышестоящим руководителем
        /// <summary>
        /// Оформление Кадры
        /// </summary>
        PENDING_FINALIZATION_BY_PERSONNEL_MANAGER = 6,//Оформление Кадры
        /// <summary>
        /// Принят
        /// </summary>
        COMPLETE = 7,//Принят
        /// <summary>
        /// Выгружено в 1С
        /// </summary>
        SENT_TO_1C = 8,//Выгружено в 1С
        /// <summary>
        /// Отклонен
        /// </summary>
        REJECTED = 9, //Отклонен
        /// <summary>
        /// Ожидает предварительное согласование ДБ
        /// </summary>
        PENDING_PREV_APPROVAL_BY_SECURITY = 10,
        /// <summary>
        /// Контроль руководителя - пакет документов на подпись.
        /// </summary>
        DOCUMENTS_SENT_TO_SIGNATURE_TO_CANDIDATE = 11,
        /// <summary>
        /// Документы подписаны кандидатом.
        /// </summary>
        DOCUMENTS_SIGNATURE_CANDIDATE_COMPLETE = 12
        
    }
}