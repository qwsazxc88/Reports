using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class PersonnelFileDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Position { get; set; }
        public bool IsEditable { get; set; }
        public string Receiver { get; set; }
        public int ReceiverId { get; set; }
        public string Sender { get; set; }
        public int SenderId { get; set; }
        public string CurrentPlace { get; set; }
        public int CurrentPlaceId { get; set; }
        public string SendPlace { get; set; }
        public int SendPlaceId { get; set; }
        public string SourcePlace { get; set; }
        public int SourcePlaceId { get; set; }
        public string Department { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public bool IsArchive { get; set; }
    }
}
