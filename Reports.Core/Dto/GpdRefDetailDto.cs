using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Список типов реквизитов.
    /// </summary>
    public class GpdRefDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Справочник реквизитов.
    /// </summary>
    public class GpdRefDetailFullDto : GpdRefDetailDto
    {
        public virtual int DTID { get; set; }
        public virtual string INN { get; set; }
        public virtual string KPP { get; set; }
        public virtual string Account { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BankBIK { get; set; }
        public virtual string CorrAccount { get; set; }
        public virtual int CreatorID { get; set; }
        public virtual string Code { get; set; }
    }
}
