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
    }
    public enum RequestAttachmentTypeEnum
    {
        //Vacation = 1,
        //Absence = 2,
        Sicklist = 1,
        //HolidayWork = 4,
        //Mission = 5,
        //Dismissal = 6,
        //TimesheetCorrection = 7,
        Employment = 2,
        Dismissal = 3,
        Vacation = 4,
        //EmploymentPen = 3,
        //EmploymentInn = 4,
        //EmploymentNdfl = 5,
    }
    public enum RequestPrintFormTypeEnum
    {
        Vacation = 1,
        Dismissal = 2,
    }
}