using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель создания/редактирования договоров.
    /// </summary>
    public class GpdContractEditModel
    {
        [Display(Name = "Номер заявки")]
        public int Id { get; set; }
        public int CreatorID { get; set; }
        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "ФИО ГПД")]
        public int PersonID { get; set; }
        public string Surname { get; set; }
        public IList<GpdContractSurnameDto> Persons { get; set; }

        [Display(Name = "Вид начисления по договору")]
        public int CTID { get; set; }
        public IList<GpdContractChargingTypesDto> ChargingTypes { get; set; }

        [Display(Name = "Вид начисления по договору")]
        public string CTName { get; set; }

        [Display(Name = "Наименование договора")]
        public string NameContract { get; set; }

        [Display(Name = "Номер договора в ЭССД")]
        public string NumContract { get; set; }

        [Display(Name = "Срок действия договора с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateEnd { get; set; }

        [Display(Name = "Дата пролонгации")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateP { get; set; }
        public System.DateTime? DatePOld { get; set; }

        [Display(Name = "Плательщик")]
        //public int PayerID { get; set; }
        public string PayerName { get; set; }
        [Display(Name = "ИНН плательщика")]
        public string PayerINN { get; set; }
        [Display(Name = "КПП плательщика")]
        public string PayerKPP { get; set; }
        [Display(Name = "Расчетный счет")]
        public string PayerAccount { get; set; }
        [Display(Name = "Банк плательщика")]
        public string PayerBankName { get; set; }
        [Display(Name = "Бик плательщика")]
        public string PayerBankBIK { get; set; }
        [Display(Name = "Кор. счет. плательщика")]
        public string PayerCorrAccount { get; set; }
        //public IList<GpdContractDetailDto> Payers { get; set; }

        [Display(Name = "Получатель")]
        //public int PayeeID { get; set; }
        public string PayeeName { get; set; }
        [Display(Name = "ИНН получателя")]
        public string PayeeINN { get; set; }
        [Display(Name = "КПП получателя")]
        public string PayeeKPP { get; set; }
        [Display(Name = "Расчетный счет")]
        public string PayeeAccount { get; set; }
        [Display(Name = "Банк получателя")]
        public string PayeeBankName { get; set; }
        [Display(Name = "Бик получателя")]
        public string PayeeBankBIK { get; set; }
        [Display(Name = "Кор. счет. получателя")]
        public string PayeeCorrAccount { get; set; }
        //public IList<GpdContractDetailDto> Payeers { get; set; }

        [Display(Name = "ID физического лица (ГПД) в ЭССД")]
        public string GPDID { get; set; }

        [Display(Name = "ID договора с физ. лицом (ГПД) в ЭССД")]
        public string GPDContractID { get; set; }

        [Display(Name = "Назначение платежа")]
        public string PurposePayment { get; set; }
        public string PurposePaymentPart { get; set; }

        [Display(Name = "Автор")]
        public string Autor { get; set; }

        [Display(Name = "Статус договора")]
        public int StatusID { get; set; }
        public IList<GpdContractStatusesDto> Statuses { get; set; }

        [Display(Name = "Состояние")]
        public string StatusName { get; set; }

        [Display(Name = "Оплата")]
        public IList<GpdContractStatusesDto> PaymentPeriods { get; set; }
        public int PaymentPeriodID { get; set; }

        [Display(Name = "Сумма договора")]
        [DisplayFormat(DataFormatString = "{0:### ### ### ###.##}", ApplyFormatInEditMode = true)]
        public System.Decimal Amount { get; set; }

        [Display(Name = "Лицевой счет получателя")]
        public string Account { get; set; }

        public IList<GpdContractDto> Contracts { get; set; }
        public bool hasErrors { get; set; }
        public string CreatorName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsFind { get; set; }
        public bool IsLong { get; set; }
        public int Operation { get; set; }
        public int DSID { get; set; }

        //права
        public IList<GpdPermissionDto> Permissions { get; set; }
    }
}
