using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class HolidayWork : AbstractEntityWithVersion//AbstractEntity
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual int Number { get; set; }
        public virtual HolidayWorkType Type { get; set; }
     
        public virtual DateTime WorkDate { get; set; }
        //public virtual int? Rate { get; set; }
        public virtual int? Hours { get; set; }
        
        public virtual TimesheetStatus TimesheetStatus { get; set; }
       

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }

        public virtual IList<HolidayWorkComment> Comments { get; set; }
        
        
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}