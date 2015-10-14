using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Dto
{
    public class KladrDto : AbstractEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Index { get; set; }
        public string AltName { get; set; }
        public string AddressType { get; set; }
        public string RegionCode { get; set; }
        public string AreaCode { get; set; }
        public string CityCode { get; set; }
        public string SettlementCode { get; set; }
        public string StreetCode { get; set; }
        public string Code { get; set; }
        public string SelectedIndex { get; set; }
    }

    public class KladrWithPostIndex
    {
        public IList<KladrDto> Kladr { get; set; }
        public string PostIndex { get; set; }
    }
}
