using System;

namespace Reports.Core.Dto
{
    public class DocumentAndAttachmentDto
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public DateTime Date { get; set; }
        public int TypeId { get; set; }
        public int SubTypeId { get; set; }
        public string Comment { get; set; }
        public DateTime? ManagerDateAccept { get; set; }
        public DateTime? PersonnelManagerDateAccept { get; set; }
        public DateTime? BudgetManagerDateAccept { get; set; }
        public DateTime? OutsourcingManagerDateAccept { get; set; }
        public bool SendEmailToBilling { get; set; }
        public int AttachId { get; set; }
        public string AttachName { get; set; }
    }
}