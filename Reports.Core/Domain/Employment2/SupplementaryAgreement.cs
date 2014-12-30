using System;

namespace Reports.Core.Domain
{
    public class SupplementaryAgreement : AbstractEntity
    {

        #region Props

        public virtual DateTime? CreateDate { get; set; }
        public virtual int? Number { get; set; }
        public virtual DateTime? OrderCreateDate { get; set; }
        public virtual int? OrderNumber { get; set; }
        public virtual PersonnelManagers PersonnelManagers { get; set; }

        #endregion

    }
}