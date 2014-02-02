using System;

namespace Reports.Core.Dto
{
    public class MissionPurchaseBookDocDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Contractor { get; set; }
        public decimal Sum { get; set; }
    }
}