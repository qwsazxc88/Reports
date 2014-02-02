using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class MissionPurchaseBookDocument : AbstractEntityWithVersion
    {
        #region Fields

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual DateTime DocumentDate { get; set; }
       

        public virtual string Number { get; set; }


        public virtual Contractor Contractor { get; set; }
        public virtual decimal Sum { get; set; }
        public virtual User Editor { get; set; }
        //public virtual IList<MissionTarget> Targets { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}