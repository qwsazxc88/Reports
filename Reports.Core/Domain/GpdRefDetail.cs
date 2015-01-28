using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// реквизит
    /// </summary>
    public class GpdRefDetail : AbstractEntityWithVersion
    {
        #region Fields
        //public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ContractorName { get; set; }
        public virtual int CreatorID { get; set; }
        //public virtual int DTID { get; set; }
        public virtual string INN { get; set; }
        public virtual string KPP { get; set; }
        public virtual string Account { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BankBIK { get; set; }
        public virtual string CorrAccount { get; set; }
        public virtual string PersonAccount { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual int? EditorID { get; set; }

        //public virtual IList<GpdRefDetailTypes> DetailTypes { get; set; }
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
    /// <summary>
    /// набор
    /// </summary>
    public class GpdDetailSet : AbstractEntityWithVersion
    {
        public virtual int CreatorID { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual int? EditorID { get; set; }
        public virtual string Name { get; set; }
        public virtual int PersonID { get; set; }
        public virtual int PayerID { get; set; }
        public virtual int PayeeID { get; set; }
        public virtual string Account { get; set; }
    }
}
