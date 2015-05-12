using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class SurchargeNoteDto
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? PersonnelsId { get; set; }
        public string PersonnelName { get; set; }
        public int? CountantId { get; set; }
        public string CountantName { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PayDay { get; set; }
        public DateTime? PersonnelDateAccept { get; set; }
        public DateTime? CountantDateAccept { get; set; }
        public int NoteType { get; set; }
    }
}
