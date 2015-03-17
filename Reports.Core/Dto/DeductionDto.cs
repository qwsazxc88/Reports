using System;

namespace Reports.Core.Dto
{
    public class DeductionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; }
        public DateTime EditDate { get; set; }
        public string Dep3Name { get; set; }
        public decimal Sum { get; set; }
        public DateTime DeductionDate { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string Kind { get; set; }
        public string Dep7Name { get; set; }
        public DateTime? DismissalDate { get; set; }
        public string Status { get; set; }
        public string IsFastDismissal { get; set; }
        public int Rn { get; set; }
        public int UploadingDocType { get; set; }
    }
}