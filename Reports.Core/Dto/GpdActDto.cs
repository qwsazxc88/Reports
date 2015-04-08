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
        public int CTType { get; set; }
        //public int DSID { get; set; }
        public string DetailName { get; set; }
        public DateTime? DateP { get; set; }
        public string DepLevel7Name { get; set; }
        public string GPDID { get; set; }
        //плательщик
        public int PayerID { get; set; }
        public string PayerName { get; set; }
        public string PayerINN { get; set; }
        public string PayerKPP { get; set; }
        public string PayerAccount { get; set; }
        public string PayerBankName { get; set; }
        public string PayerBankBIK { get; set; }
        public string PayerCorrAccount { get; set; }
        //получатель
        public int PayeeID { get; set; }
        public string PayeeName { get; set; }
        public string PayeeINN { get; set; }
        public string PayeeKPP { get; set; }
        public string PayeeAccount { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBIK { get; set; }
        public string PayeeCorrAccount { get; set; }
        public int PAccountID { get; set; }
        public string Account { get; set; }
        public bool flgRed { get; set; }
    }
    /// <summary>
    /// Список статусов договора.
    /// </summary>
    public class GpdActStatusesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Комментарии к акту ГПД.
    /// </summary>
    public class GpdActCommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActId { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }
    }

    public class GpdActEqualityComparerByPayeeId : IEqualityComparer<GpdActDto>
    {
        public bool Equals(GpdActDto x, GpdActDto y)
        {
            return (x.PayeeID == y.PayeeID);
        }

        public int GetHashCode(GpdActDto obj)
        {
            return (obj.PayeeID+obj.PayeeName).GetHashCode();
        }
    }
    public class GpdActEqualityComparerByPayeeIdandCTName : IEqualityComparer<GpdActDto>
    {
        public bool Equals(GpdActDto x, GpdActDto y)
        {
            return (x.PayeeID == y.PayeeID && x.CTName==y.CTName);
        }

        public int GetHashCode(GpdActDto obj)
        {
            return (obj.PayeeID + obj.PayeeName+obj.CTName).GetHashCode();
        }
    }

}
