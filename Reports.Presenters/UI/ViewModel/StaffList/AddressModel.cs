using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;
using System.Web.Mvc;

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

        [Display(Name = "Адрес")]
        public string Address { get; set; }


        [Display(Name = "Дом")]
        public int HouseType { get; set; }
        public IList<IdNameDto> HouseTypes { get; set; }

        [Display(Name = "Корпус")]
        public int BuildType { get; set; }
        public IList<IdNameDto> BuildTypes { get; set; }

        [Display(Name = "Квартира")]
        public int FlatType { get; set; }
        public IList<IdNameDto> FlatTypes { get; set; }


        [Display(Name = "№ дома")]
        public string HouseNumber { get; set; }

        [Display(Name = "№ корпуса")]
        public string BuildNumber { get; set; }

        [Display(Name = "№ квартиры")]
        public string FlatNumber { get; set; }

        [Display(Name = "Почтовый индекс")]
        public string PostIndex { get; set; }

        //для сообщений
        public string errorMessage { get; set; }
        public string Code { get; set; }
        public int AddressType { get; set; }
    }
}
