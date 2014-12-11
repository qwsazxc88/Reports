namespace Reports.Core.Enum
{
    public enum EmploymentStatus
    {
        PENDING_APPROVAL_BY_SECURITY = 1,
        PENDING_REPORT_BY_TRAINER = 2,
        PENDING_APPLICATION_LETTER = 3,
        PENDING_APPROVAL_BY_MANAGER = 4,
        PENDING_APPROVAL_BY_HIGHER_MANAGER = 5,
        //
        PENDING_FINALIZATION_BY_PERSONNEL_MANAGER = 6,
        COMPLETE = 7,
        SENT_TO_1C = 8,
        REJECTED = 9
    }
}