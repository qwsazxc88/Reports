using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель добавления/редактирования справочника реквизитов.
    /// </summary>
    public class GpdRefDetailEditModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
        //набор реквизитов
        public IList<GpdDetailSetsListDto> SetInfo { get; set; }

        //физическое лицо
        [Display(Name = "ФИО физ. лица")]
        public int PersonID { get; set; }
        public string Surname { get; set; }
        public IList<GpdContractSurnameDto> Persons { get; set; }

        //реквизиты плательщика
        [Display(Name = "Плательщик")]
        public int PayerID { get; set; }
        public IList<GpdDetailDto> PayerInfo { get; set; }

        public string PayerName { get; set; }
        public string PayerINN { get; set; }
        public string PayerKPP { get; set; }
        public string PayerAccount { get; set; }
        public string PayerBankName { get; set; }
        public string PayerBankBIK { get; set; }
        public string PayerCorrAccount { get; set; }


        //реквизиты получателя
        [Display(Name = "Получатель")]
        public int PayeeID { get; set; }
        public IList<GpdDetailDto> PayeerInfo { get; set; }

        public string PayeerName { get; set; }
        public string PayeerINN { get; set; }
        public string PayeerKPP { get; set; }
        public string PayeerAccount { get; set; }
        public string PayeerBankName { get; set; }
        public string PayeerBankBIK { get; set; }
        public string PayeerCorrAccount { get; set; }

        //реквизиты
        public IList<GpdDetailDto> RefDetails { get; set; }

        [Display(Name = "Расчетный/лицевой счет получателя")]
        public string Account { get; set; }

        [Display(Name = "Тип реквизита")]
        public int DTID { get; set; }
        public string TypeName { get; set; }
        public IList<GpdRefDetailDto> DetailTypes { get; set; }

        [Display(Name = "Наименование")]
        public string DetailName { get; set; }

        [Display(Name = "Выбор реквизита")]
        public int DetailId { get; set; }

        [Display(Name = "ИНН")]
        public string INN { get; set; }

        [Display(Name = "КПП")]
        public string KPP { get; set; }

        [Display(Name = "Расчетный счет")]
        public string DetailAccount { get; set; }

        [Display(Name = "Банк")]
        public string BankName { get; set; }

        [Display(Name = "Банк БИК")]
        public string BankBIK { get; set; }

        [Display(Name = "Банк кор/счет")]
        public string CorrAccount { get; set; }

        //[Display(Name = "Код банка")]
        //public string Code { get; set; }

        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }
        public bool hasErrors { get; set; }
        public int StatusID { get; set; }
        public int Operation { get; set; }
        public int CreatorID { get; set; }
        public int DetailCreatorID { get; set; }
        public int NewRow { get; set; } //1 - новый реквизит

        //public IList<GpdRefDetailFullDto> Documents { get; set; }


        //права
        public IList<GpdPermissionDto> Permissions { get; set; }

    }
}
