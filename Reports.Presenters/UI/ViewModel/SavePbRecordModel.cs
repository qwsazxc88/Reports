namespace Reports.Presenters.UI.ViewModel
{
    public class SavePbRecordModel
    {
        public int DocumentId { get; set; }
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int ReportId { get; set; }
        public int CostTypeId { get; set; }
        public decimal Sum { get; set; }
        public decimal? SumNds { get; set; }
        public decimal AllSum { get; set; }
        public string RequestNumber { get; set; }
    }
    public class DeletePbRecordModel
    {
        public int Id { get; set; }
    }

}