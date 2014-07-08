using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class MissionPurchaseBookRecord : AbstractEntityWithVersion
    {
        #region Fields

        public virtual DateTime EditDate { get; set; }
        public virtual MissionPurchaseBookDocument Document { get; set; }
        public virtual MissionOrder MissionOrder { get; set; }
        public virtual MissionReportCostType MissionReportCostType { get; set; }
        public virtual MissionReportCost MissionReportCost { get; set; }
        public virtual decimal Sum { get; set; }
        public virtual decimal? SumNds { get; set; }
        public virtual decimal AllSum { get; set; }
        public virtual string RequestNumber { get; set; }
        public virtual User User { get; set; }
        public virtual User Editor { get; set; }
        //public virtual IList<MissionTarget> Targets { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}