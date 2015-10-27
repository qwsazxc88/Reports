using System;
using System.Collections.Generic;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник адресов.
    /// </summary>
    public class RefAddresses : AbstractEntityWithVersion
    {
        public virtual string Address { get; set; }
        public virtual string PostIndex { get; set; }
        public virtual string RegionCode { get; set; }
        public virtual string RegionName { get; set; }
        public virtual string AreaCode { get; set; }
        public virtual string AreaName { get; set; }
        public virtual string CityCode { get; set; }
        public virtual string CityName { get; set; }
        public virtual string SettlementCode { get; set; }
        public virtual string SettlementName { get; set; }
        public virtual string StreetCode { get; set; }
        public virtual string StreetName { get; set; }
        public virtual int HouseType { get; set; }
        public virtual string HouseNumber { get; set; }
        public virtual int BuildType { get; set; }
        public virtual string BuildNumber { get; set; }
        public virtual int FlatType { get; set; }
        public virtual string FlatNumber { get; set; }
        public virtual bool IsUsed { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}
