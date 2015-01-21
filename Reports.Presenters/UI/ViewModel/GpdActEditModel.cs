using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель для редактирования актов ГПД.
    /// </summary>
    public class GpdActEditModel
    {
        [Display(Name = "№ заявки")]
        public int Id { get; set; }

        [Display(Name = "Дата акта")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? ActDate { get; set; }

        [Display(Name = "№ акта в ЭССД")]
        public string ActNumber { get; set; }

        [Display(Name = "ФИО")]
        public string Surname { get; set; }

        [Display(Name = "Наименование договора")]
        public string NameContract { get; set; }

        [Display(Name = "Номер договора")]
        public string NumContract { get; set; }

        [Display(Name = "Подразделение 3 ур.")]
        public string DepLevel3Name { get; set; }

        [Display(Name = "Месяц начисления")]
        [DisplayFormat(DataFormatString = "{0:MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? ChargingDate { get; set; }

        [Display(Name = "Оплата за период с ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateEnd { get; set; }

        [Display(Name = "Сумма начисленная")]
        [DisplayFormat(DataFormatString = "{0:### ### ### ###.##}", ApplyFormatInEditMode = true)]
        public System.Decimal Amount { get; set; }

        [Display(Name = "Сумма к выплате")]
        [DisplayFormat(DataFormatString = "{0:### ### ### ###.##}", ApplyFormatInEditMode = true)]
        public decimal AmountPayment { get; set; }

        [Display(Name = "Дата платежного поручения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? POrderDate { get; set; }

        [Display(Name = "Назначение платежа")]
        public string PurposePayment { get; set; }

        [Display(Name = "№ заявки в ЭССС")]
        public string ESSSNum { get; set; }

        [Display(Name = "Автор")]
        public string Autor { get; set; }

        [Display(Name = "Состояние")]
        public string StatusName { get; set; }
        public int StatusID { get; set; }

        [Display(Name = "Срок действия договора с ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? ContractBeginDate { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? ContractEndDate { get; set; }

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
        [Display(Name = "Лицевой счет получателя")]
        public string Account { get; set; }


        [Display(Name = "Набор реквизитов")]
        public int DSID { get; set; }
        public string DetailName { get; set; }

        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }
        public bool hasErrors { get; set; }

        
        public int GCID { get; set; }
        public int CreatorID { get; set; }
        public int Operation { get; set; }
        public bool IsCancel { get; set; }

        public IList<GpdActCommentDto> Comments { get; set; }
        public string CommentStr { get; set; }

        //права
        public IList<GpdPermissionDto> Permissions { get; set; }
    }
}
