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

        // Order Scans

        DismissalOrderScan = 103,
        VacationOrderScan = 104,
        ChildVacationOrderScan = 105,
        MissionOrderScan = 118,

        UnsignedDismissalOrderScan = 153,
        UnsignedVacationOrderScan = 154,

    }
  //dbo.Absence (неявки)                  RequestTypeId = "",(Нет печатной формы)
  //dbo.Dismissal (увольнения)            RequestTypeId = 2, (приказ)
  //dbo.Employment (прием на работу)      RequestTypeId = 3, (приказ)
  //dbo.HolidayWork (работа в выходные)   RequestTypeId = "",(Нет печатной формы)
  //dbo.Mission (командировки)            RequestTypeId = 4, (приказ) и RequestTypeId = 5, (удостоверение)
  //dbo.Sicklist (больничные)             RequestTypeId = "",(Нет печатной формы)
  //dbo.Vacation (отпуска)                RequestTypeId = 1, (приказ)

    public enum RequestPrintFormTypeEnum
    {
        Vacation = 1,
        Dismissal = 2,
        Employment = 3,
        MissionOrder = 4,
        MissionCertificate = 5,
        ChildVacation = 6,
    }
    public enum DeductionTypeEnum
    {
        Deduction = 1,
        DeductionOnDismissal = 2,
        DeductionAfterDismissal = 3,
    }
}