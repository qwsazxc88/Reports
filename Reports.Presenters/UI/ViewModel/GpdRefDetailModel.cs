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

        [Display(Name = "Поиск по наименованию")]
        public string Name { get; set; }

        [Display(Name = "Поиск по названию контрагента")]
        public string ContractorName { get; set; }

        public IList<GpdDetailDto> Documents { get; set; }



        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }
        public bool hasErrors { get; set; }

        //права
        public IList<GpdPermissionDto> Permissions { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }
    }
}
