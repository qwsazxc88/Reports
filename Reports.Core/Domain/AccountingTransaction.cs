namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class AccountingTransaction : AbstractEntityWithVersion
    {
        #region Fields

        public virtual MissionReportCost Cost { get; set; }
        public virtual Account DebitAccount { get; set; }
        public virtual Account CreditAccount { get; set; }
        public virtual decimal Sum { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}