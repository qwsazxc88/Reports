using System;
namespace Reports.Core.Dto
{
    public class AppointmentDto
    {
                public int AppNumber {get;set;}
                public int ReportNumber { get; set; }
                public int Id {get;set;}
                public DateTime EditDate { get; set; }
                public string UserName {get;set;}
                public string PositionName {get;set;}
                public string ManDep7Name { get; set; }
                public string CanPosition {get;set;}
                public string Dep3Name { get; set; }
                public string Dep7Name {get;set;}
                public string Period {get;set;}
                public string Schedule {get;set;}
                public decimal Salary {get;set;}
                public DateTime? DesirableBeginDate {get;set;}
                public string Reason {get;set;}
                public int Number {get;set;}
    }
}