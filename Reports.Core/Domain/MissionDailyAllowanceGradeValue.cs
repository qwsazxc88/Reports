using System;

namespace Reports.Core.Domain
{
    public class MissionDailyAllowanceGradeValue : AbstractEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual MissionDailyAllowance DailyAllowance { get; set; }
        public virtual MissionGraid Grade { get; set; }
        public virtual DateTime GradeDate { get; set; }
        public virtual decimal Amount { get; set; }
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