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
        public int PayType { get; set; }
        public int MonthId { get; set; }
        public string UserName { get; set; }
        public int? PersonnelsId { get; set; }
        public string PersonnelName { get; set; }
        public string PersonnelManagerBankName { get; set; }
        public int? CountantId { get; set; }
        public string CountantName { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PayDay { get; set; }
        public DateTime? PayDayEnd { get; set; }
        public DateTime? DismissalDate { get; set; }        
        public DateTime? PersonnelDateAccept { get; set; }
        public DateTime? CountantDateAccept { get; set; }
        public DateTime? PersonnelManagerBankDateAccept { get; set; }
        public int NoteType { get; set; }
        public string Number { get; set; }
        public string CreatorName { get; set; }
        public string CreatorDepartment3 { get; set; }
        public string CreatorDepartment { get; set; }
        public string Position { get; set; }
        public string Dep3Name { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public string Status { get; set; }
    }
}
