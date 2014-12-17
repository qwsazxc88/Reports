using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Для списка актов ГПД.
    /// </summary>
    public class GpdActDto
    {
        public int Id { get; set; }
        public int CreatorID { get; set; }
        public DateTime? ActDate { get; set; }
        public string ActNumber { get; set; }
        public int GCCount { get; set; }
        public string Surname { get; set; }
        public string NameContract { get; set; }
        public string NumContract { get; set; }
        public DateTime? ContractBeginDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string DepLevel3Name { get; set; }
        public DateTime? ChargingDate { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPayment { get; set; }
        public DateTime? POrderDate { get; set; }
        public string PurposePayment { get; set; }
        public string ESSSNum { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int GCID { get; set; }
        public string CTName { get; set; }
        public DateTime? DateP { get; set; }
        public string DepLevel7Name { get; set; }
        public string GPDID { get; set; }
    }
    /// <summary>
    /// Список статусов договора.
    /// </summary>
    public class GpdActStatusesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
