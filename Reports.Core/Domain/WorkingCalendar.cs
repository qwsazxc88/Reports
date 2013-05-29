using System;

namespace Reports.Core.Domain
{
    public class WorkingCalendar : AbstractEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual DateTime Date { get; set; }
        public virtual int? IsWorkingHours { get; set; }
        #endregion

        #region Constructors
        #endregion
        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        #endregion
    }
}