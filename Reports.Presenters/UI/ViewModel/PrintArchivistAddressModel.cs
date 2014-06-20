using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class PrintArchivistAddressModel
    {
        [Display(Name = "Архивариус")]
        public int ArchivistId { get; set; }
        public List<IdNameDto> Archivists { get; set; }
        //[Display(Name = "Количество")]
        //public int? Count { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        public string AddressList { get; set; }
        public string Error { get; set; }
    }
}
