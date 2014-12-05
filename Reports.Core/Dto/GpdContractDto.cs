using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Список статусов договора.
    /// </summary>
    public class GpdContractStatusesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Список видов начисления.
    /// </summary>
    public class GpdContractChargingTypesDto : GpdContractStatusesDto
    {
    }
    /// <summary>
    /// Список физических лиц.
    /// </summary>
    public class GpdContractSurnameDto : GpdContractStatusesDto
    {
    }
    /// <summary>
    /// Списосок договоров.
    /// </summary>
    public class GpdContractDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int PersonID { get; set; }
        public int CTID { get; set; }
        public string CTName { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public string NumContract { get; set; }
        public string NameContract { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateP { get; set; }
        public DateTime? DatePOld { get; set; }
        public int PayeeID { get; set; }
        public int PayerID { get; set; }
        public string GPDID { get; set; }
        public string PurposePayment { get; set; }
        public string Surname { get; set; }
        public int CreatorID { get; set; }
        public bool IsDraft { get; set; }
        public string Autor { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreateDate { get; set; }
    }
    /// <summary>
    /// Список реквизитов.
    /// </summary>
    public class GpdContractDetailDto : GpdContractStatusesDto
    {
        public virtual int DTID { get; set; }
    }
}
