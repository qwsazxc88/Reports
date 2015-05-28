using System;
namespace Reports.Core.Dto
{
    public class AppointmentDto
    {
                public int AppNumber { get; set; }
                //public int ReportNumber { get; set; }
                public int Id {get;set;}
                public DateTime CreateDate { get; set; }
                public DateTime EditDate { get; set; }
                public string UserName { get; set; }
                public string PositionName { get; set; }
                public string ManDep7Name { get; set; }
                public string ManDep3Name { get; set; }
                public string CanPosition {get;set;}
                public string Dep3Name { get; set; }
                public string Dep7Name { get; set; }
                //public string Period {get;set;}
                public string Schedule { get; set; }
                public decimal Salary { get; set; }
                public DateTime? DesirableBeginDate { get; set; }
                public string Reason { get; set; }
                public int ReasonId { get; set; }
                public int? RId { get; set; }
                public int? RNumber { get; set; }
                public string RStaffAccept { get; set; }
                public string RName { get; set; }
                public string Phone { get; set; }
                public string Email { get; set; }
                public string RApprove { get; set; }
                public string RReject { get; set; }
                public string StaffName { get; set; }
                public int Number { get; set; }
                public string Status { get; set; }
                public bool BankAccountantAccept { get; set; }
                public int BankAccountantAcceptCount { get; set; }
                public int SecondNumber { get; set; }
                public string EducationStatus { get; set; }
                public int Recruter { get; set; }
                public string CandidateFIO { get; set; }
                public Reports.Core.Enum.EmploymentStatus EmploymentStatus { get; set; }
    }
}