namespace Reports.Core.Dto
{
    public class MissionPurchaseBookRecordDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ReportNumber { get; set; }
        public int OrderNumber { get; set; }
        public decimal Sum { get; set; }
        public decimal SumNds { get; set; }
        public decimal AllSum { get; set; }
        public string CostType { get; set; }
        public string RequestNumber { get; set; }
        public bool IsEditable { get; set; }
    }
}