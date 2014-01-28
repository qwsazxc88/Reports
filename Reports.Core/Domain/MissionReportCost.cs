using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class MissionReportCost : AbstractEntityWithVersion
    {
        #region Fields

        public virtual MissionReport Report { get; set; }
        public virtual MissionReportCostType Type { get; set; }
        public virtual bool IsCostFromOrder { get; set; }
        public virtual bool IsCostFromPurchaseBook { get; set; }
        public virtual int? Cnt { get; set; }
        public virtual decimal? Sum { get; set; }
        public virtual decimal? UserSum { get; set; }
        public virtual decimal? BookOfPurchaseSum { get; set; }
        public virtual decimal? AccountantSum { get; set; }

        public virtual IList<AccountingTransaction> AccountingTransactions { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}