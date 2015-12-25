using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Надбавки
    /// </summary>
    public class StaffEstablishedPostChargeLinksDto
    {
        public int Id { get; set; }
        public int ChargeId { get; set; }
        public string ChargeName { get; set; }
        public int SEPRequestId { get; set; }
        public int SEPId { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPrev { get; set; }
        public string UnitName { get; set; }
        public bool IsUsed { get; set; }
        public bool IsNeeded { get; set; }
        public int ActionId { get; set; }
    }
}
