namespace Reports.Core.Enum
{
    public enum RequestStatusEnum
    {
        NotApproved = 1,
        ApprovedByUser = 2,
        ApprovedByManager = 3,
        ApprovedByPersonnel = 4,
        SendTo1C = 5
    }

    public enum RequestTypeEnum
    {
        Vacation = 1,
        Absence = 2,
        Sicklist = 3,
        HolidayWork = 4,
        Mission = 5,
        Dismissal = 6,
        TimesheetCorrection = 7,
        Employment = 8,
        ChildVacation = 9,
        Deduction = 10,
        MissionOrder = 11,
        MissionReport = 12,
        Appointment = 13,
        AppointmentReport = 14,
        ClearanceChecklist = 15,
        HelpServiceRequest = 16,
        HelpQuestionRequest = 17,
        HelpPersonnelBillingRequest = 18,
        Surcharge = 19,
        SurchargeNote0=20,
        SurchargeNote1=21,
        SurchargeNote2 = 22,
        SurchargeNote3 = 23,
        SurchargeNote4 = 24,
        SurchargeNote5 = 25,
        SurchargeNote6 = 26,
    }
    public enum RequestAttachmentTypeEnum
    {
        //Vacation = 1,
        //Absence = 2,
        Sicklist = 1,
        //HolidayWork = 4,        
        //Dismissal = 6,
        //TimesheetCorrection = 7,
        Employment = 2,
        Dismissal = 3,
        Vacation = 4,
        //EmploymentPen = 3,
        //EmploymentInn = 4,
        //EmploymentNdfl = 5,
        ChildVacation = 5,
        MissionReport = 6,
        AppointmentReport = 7,
        MissionReportCost = 8,
        HelpTemplate = 9,
        HelpServiceRequestTemplate = 10,
        HelpServiceRequest = 11,
        HelpPersonnelBillingRequest = 12,

        // Order Scans

        DismissalOrderScan = 103,
        VacationOrderScan = 104,
        ChildVacationOrderScan = 105,
        MissionOrderScan = 118,

        UnsignedDismissalOrderScan = 153,
        UnsignedVacationOrderScan = 154,

        // Документы из анкеты кандидата в приеме
        /// <summary>
        /// Фото кандидата
        /// </summary>
        Photo = 210,//201,
        /// <summary>
        /// Скан ИНН
        /// </summary>
        INNScan = 212,//202,
        /// <summary>
        /// Скан СНИЛС
        /// </summary>
        SNILSScan = 213,//203,
        /// <summary>
        /// Скан справки об инвалидности
        /// </summary>
        DisabilityCertificateScan = 214,//204,
        /// <summary>
        /// Скан паспорта
        /// </summary>
        InternalPassportScan = 211,
        /// <summary>
        /// Скан диплома об образовании
        /// </summary>
        HigherEducationDiplomaScan = 221,
        /// <summary>
        /// Скан документов о послевузовском обазовании
        /// </summary>
        PostGraduateEducationDiplomaScan = 222,
        /// <summary>
        /// Скан докумнетов о дополнительном образовании
        /// </summary>
        CertificationScan = 223,
        /// <summary>
        /// Скан документов о повышении квалификации
        /// </summary>
        Training = 224,
        /// <summary>
        /// Свидетельсвто о браке
        /// </summary>
        MarriageCertificateScan = 231,
        /// <summary>
        /// свидетельство о рождении детей
        /// </summary>
        ChildBirthCertificateScan = 232,
        //SpousePassportScan = 233,
        /// <summary>
        /// Скан  военного билета
        /// </summary>
        MilitaryCardScan = 241,
        /// <summary>
        /// Скан мобилизационного талона
        /// </summary>
        MobilizationTicketScan = 242,
        /// <summary>
        /// Скан трудовой книжки
        /// </summary>
        WorkbookScan = 215,//251,
        /// <summary>
        /// Скан вкладыша трудовой книжки
        /// </summary>
        WorkbookSupplementScan = 216,//252,
        /// <summary>
        /// Скан согласия на обработку персональных данных
        /// </summary>
        PersonalDataProcessingScan = 261,
        /// <summary>
        /// Скан текста о достоверности сведений
        /// </summary>
        InfoValidityScan = 262,
        /// <summary>
        /// Заявление о приеме
        /// </summary>
        ApplicationLetterScan = 271,

        //документы распечатанные и отсканированные кандидатом в приеме
        EmploymentContractScan = 272,
        OrderOnReceptionScan = 273,
        CandidateT2Scan = 274,
        ContractMatResponsibleScan = 275,
        PersonalDataScan = 276,
        DataObligationScan = 277,
        EmploymentFileScan = 278,
        RegisterPersonalRecordScan = 279,
        InstructionOfSecretScan = 280,
        InstructionEnsuringSafetyScan = 281,
        AgreePersonForCheckingScan = 282,
        CashWorkAddition1Scan = 283,
        CashWorkAddition2Scan = 284,
        ObligationTradeSecretScan = 285,
        Certificate182HScan = 286,
        Certificate2NDFLScan = 287,

        // end Employment documents

        // Dismissal Additional Documents Scans

        T2Scan = 201,        
        DismissalAgreementScan = 202,
        F182NScan = 203,
        F2NDFLScan = 204,
        WorkbookRequestScan = 205,
        

        UnsignedT2Scan = 251,        
        UnsignedDismissalAgreementScan = 252,

        //SurchargeNote
        SurchargeNoteAttachment= 288,
        //StaffMovements
        StaffMovements =289,
        //VacationReturn
        VacationReturn = 290
    }
  //dbo.Absence (неявки)                  RequestTypeId = "",(Нет печатной формы)
  //dbo.Dismissal (увольнения)            RequestTypeId = 2, (приказ)
  //dbo.Employment (прием на работу)      RequestTypeId = 3, (приказ)
  //dbo.HolidayWork (работа в выходные)   RequestTypeId = "",(Нет печатной формы)
  //dbo.Mission (командировки)            RequestTypeId = 4, (приказ) и RequestTypeId = 5, (удостоверение)
  //dbo.Sicklist (больничные)             RequestTypeId = "",(Нет печатной формы)
  //dbo.Vacation (отпуска)                RequestTypeId = 1, (приказ)
    public enum EmploymentCommentTypeEnum
    {
        Managers = 1,
        PersonnelManagers = 2,
        BackgroundCheck = 3
    }
    public enum RequestPrintFormTypeEnum
    {
        Vacation = 1,
        Dismissal = 2,
        Employment = 3,
        MissionOrder = 4,
        MissionCertificate = 5,
        ChildVacation = 6,
        ServiceRequest = 7,  //инфоуслуги
        StaffMovementsAgreement =8,
        StaffMovementsAgreementAddition = 9,
        StaffMovementsOrder = 10,
    }
    public enum DeductionTypeEnum
    {
        Deduction = 1,
        DeductionOnDismissal = 2,
        DeductionAfterDismissal = 3,
    }
}