using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class TimesheetCorrection : AbstractEntityWithVersion //AbstractEntity
    {
        #region Fields

        public virtual DateTime CreateDate { get; set; }
        public virtual int Number { get; set; }

        public virtual DateTime EventDate { get; set; }
        public virtual TimesheetCorrectionType Type { get; set; }
        public virtual int Hours { get; set; }

        public virtual TimesheetStatus TimesheetStatus { get; set; }

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }

        public virtual IList<TimesheetCorrectionComment> Comments { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}