using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class GpdRefDetail : AbstractEntityWithVersion
    {
        #region Fields
        //public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int CreatorID { get; set; }
        public virtual int DTID { get; set; }
        public virtual string INN { get; set; }
        public virtual string KPP { get; set; }
        public virtual string Account { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BankBIK { get; set; }
        public virtual string CorrAccount { get; set; }
        public virtual string Code { get; set; }

        //public virtual IList<GpdRefDetailTypes> DetailTypes { get; set; }
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}
