using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class AddressModel
    {
        [Display(Name = "Регион")]
        public string RegionCode { get; set; }
        public IList<KladrDto> Regions { get; set; }

        [Display(Name = "Район")]
        public string AreaCode { get; set; }
        public IList<KladrDto> Areas { get; set; }

        [Display(Name = "Город")]
        public string CityCode { get; set; }
        public IList<KladrDto> Cityes { get; set; }

        [Display(Name = "Населенный пункт")]
        public string SettlementCode { get; set; }
        public IList<KladrDto> Settlements { get; set; }

        [Display(Name = "Улица")]
        public string StreetCode { get; set; }
        public IList<KladrDto> Streets { get; set; }


        //для сообщений
        public string errorMessage { get; set; }
    }
}
