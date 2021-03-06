﻿using System;
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
        public int PersonID { get; set; }
        public string SNILS { get; set; }
        public string LongName { get; set; }
        //плательщик
        public string PayerName { get; set; }
        public string PayerINN { get; set; }
        public string PayerKPP { get; set; }
        public string PayerAccount { get; set; }
        public string PayerBankName { get; set; }
        public string PayerBankBIK { get; set; }
        public string PayerCorrAccount { get; set; }
        //получатель
        public string PayeeName { get; set; }
        public string PayeeINN { get; set; }
        public string PayeeKPP { get; set; }
        public string PayeeAccount { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBIK { get; set; }
        public string PayeeCorrAccount { get; set; }
        public string Account { get; set; }
    }
    /// <summary>
    /// Списосок договоров.
    /// </summary>
    public class GpdContractDto
    {
        public int Id { get; set; }
        public int CreatorID { get; set; }
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
        public string GPDID { get; set; }
        public string GPDContractID { get; set; }
        public string PurposePayment { get; set; }
        public string PurposePaymentPart { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Surname { get; set; }
        public string Autor { get; set; }
        public string DepLevel3Name { get; set; }
        public string DepLevel7Name { get; set; }
        public bool IsLong {get;set;}
        //плательщик
        public int PayerID { get; set; }
        public string PayerName { get; set; }
        public string PayerINN { get; set; }
        public string PayerKPP { get; set; }
        public string PayerAccount { get; set; }
        public string PayerBankName { get; set; }
        public string PayerBankBIK { get; set; }
        public string PayerCorrAccount { get; set; }
        public string PayerContractor { get; set; }
        //получатель
        public int PayeeID { get; set; }
        public string PayeeName { get; set; }
        public string PayeeINN { get; set; }
        public string PayeeKPP { get; set; }
        public string PayeeAccount { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBIK { get; set; }
        public string PayeeCorrAccount { get; set; }
        public string PayeeContractor { get; set; }
        //лицевой счет
        public int PAccountID { get; set; }
        public string PersonAccount { get; set; }
        public string Account { get; set; }
        public string PAName { get; set; }

        public int PaymentPeriodID { get; set; }
        public decimal Amount { get; set; }
        public bool flgRed { get; set; }
        public System.DateTime? SendTo1C { get; set; }
    }
    /// <summary>
    /// Список реквизитов.
    /// </summary>
    public class GpdContractDetailDto : GpdContractStatusesDto
    {
        public string LongName { get; set; }
        public string ContractorName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string Account { get; set; }
        public string PersonAccount { get; set; }
        public string BankName { get; set; }
        public string BankBIK { get; set; }
        public string CorrAccount { get; set; }
    }
}
