using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class Employment : AbstractEntityWithVersion //AbstractEntity
    {
        #region Fields

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime BeginDate { get; set; }
        //public virtual DateTime? EndDate { get; set; }
        //public virtual int DaysCount { get; set; }

        public virtual int Number { get; set; }
        public virtual EmploymentType Type { get; set; }
        public virtual EmploymentHoursType HoursType { get; set; }
        public virtual EmploymentAddition Addition { get; set; }
        public virtual Position Position { get; set; }
        //public virtual EmploymentCompensationType CompensationType { get; set; }
        public virtual decimal Salary { get; set; }
        public virtual int? Probaion { get; set; }
        public virtual string Reason { get; set; }

        public virtual TimesheetStatus TimesheetStatus { get; set; }

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }

        public virtual IList<EmploymentComment> Comments { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}