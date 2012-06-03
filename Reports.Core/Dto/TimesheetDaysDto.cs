using System;

namespace Reports.Core.Dto
{
    public class TimesheetDaysDto
    {
        public int Id { get; set; }
        public int Version  { get; set; }   
        public int Number { get; set; }        
        public float Hours  { get; set; }             
        public int StatusId { get; set; }
        public int TimesheetId { get; set; }
        public string Status { get; set; }                
        public DateTime Month { get; set; }
        public int UserId { get; set; }     
        public string Name { get; set; }
        //public string LastName { get; set; }
        //public string MiddleName { get; set; }
        public string Code { get; set; }
    }
}