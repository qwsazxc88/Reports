namespace Reports.Core.Dto
{
    public class GradeAmountDto
    {
        public int GradeId { get; set; }
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }
    public class GradeAmountNameDto : GradeAmountDto
    {
        public string Name { get; set; }
    }
}