using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель для всплывающего окна справочника реквизитов
    /// </summary>
    public class GpdRefDetailDialogModel
    {
        //реквизиты
        //public IList<GpdDetailDto> RefDetails { get; set; }

        //[Display(Name = "Тип реквизита")]
        //public int DTID { get; set; }
        //public string TypeName { get; set; }
        //public IList<GpdRefDetailDto> DetailTypes { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Наименование контрагента")]
        public string ContractorName { get; set; }

        //[Display(Name = "Выбор реквизита")]
        public int Id { get; set; }
        public int DetailID { get; set; }
        public int DetailType { get; set; }

        [Display(Name = "ИНН")]
        public string INN { get; set; }

        [Display(Name = "КПП")]
        public string KPP { get; set; }

        [Display(Name = "Расчетный счет")]
        public string Account { get; set; }

        [Display(Name = "Банк")]
        public string BankName { get; set; }

        [Display(Name = "Банк БИК")]
        public string BankBIK { get; set; }

        [Display(Name = "Лицевой счет")]
        public string PersonAccount { get; set; }

        [Display(Name = "Банк кор/счет")]
        public string CorrAccount { get; set; }

        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }

        public int CreatorID { get; set; }
    }
}
