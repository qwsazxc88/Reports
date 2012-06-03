using System;

namespace Reports.Core.Dto
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
    }
}