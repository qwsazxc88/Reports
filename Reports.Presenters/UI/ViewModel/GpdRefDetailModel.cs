using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель просмотра справочника реквизитов.
    /// </summary>
    public class GpdRefDetailModel
    {
        public int Id { get; set; }
        public int CreatorID { get; set; }

        [Display(Name = "Тип реквизита")]
        public int DTID { get; set; }
        public string TypeName { get; set; }
        public IList<GpdRefDetailDto> DetailTypes {get; set;}

        [Display(Name = "Поиск по наименованию")]
        public string Name { get; set; }

        public IList<GpdRefDetailFullDto> Documents { get; set; }

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

        [Display(Name = "Банк кор/счет")]
        public string CorrAccount { get; set; }

        [Display(Name = "Код банка")]
        public string Code { get; set; }

        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }
        public bool hasErrors { get; set; }

        //права
        public IList<GpdPermissionDto> Permissions { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }
    }
}
