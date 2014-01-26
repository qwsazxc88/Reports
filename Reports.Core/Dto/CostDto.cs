using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class CostDto
    {
        public int? Number { get; set; }
        public int CostTypeId { get; set; }
        public string Name { get; set; }
        public int? Count { get; set; }
        public decimal? GradeSum { get; set; }
        public decimal? UserSum { get; set; }
        public decimal? PurchaseBookSum { get; set; }
        public decimal? AccountantSum { get; set; }
        public int CostId { get; set; }
        public bool IsEditable { get; set; }
        public bool IsTransactionAvailable { get; set; }
        public TransactionDto[] Trans { get; set; }
        //public JsonTransList Trans { get; set; }

        public int SortOrder { get; set; }
    }
    public class TransactionDto
    {
        public string Debit { get; set; }
        public string Credit { get; set; }
        public decimal Sum { get; set; }
    }
    public class JsonCostsList
    {
        public CostDto[] List { get; set; }
    }
    //public class JsonTransList
    //{
    //    public TransactionDto[] List { get; set; }
    //}
}