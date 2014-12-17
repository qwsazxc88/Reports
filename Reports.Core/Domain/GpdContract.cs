using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class GpdContract : AbstractEntityWithVersion
    {
        #region Fields
        //public virtual int Id { get; set; }
        public virtual int DepartmentId { get; set; }
        public virtual int PersonID { get; set; }
        public virtual int CTID { get; set; }
        public virtual int StatusID { get; set; }
        public virtual string NumContract { get; set; }
        public virtual string NameContract { get; set; }
        public virtual DateTime? DateBegin { get; set; }
        public virtual DateTime? DateEnd { get; set; }
        public virtual int PayeeID { get; set; }
        public virtual int PayerID { get; set; }
        public virtual string GPDID { get; set; }
        public virtual string PurposePayment { get; set; }
        public virtual int CreatorID { get; set; }
        public virtual DateTime? DateP { get; set; }
        public virtual bool IsLong { get; set; }

        //журнал изменений даты пролонгации
        public virtual IList<GpdMagProlongation> MagEntities { get; set; }
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}
