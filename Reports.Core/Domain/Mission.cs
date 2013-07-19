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
    public class Mission : AbstractEntityWithVersion//AbstractEntity
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime BeginDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int DaysCount { get; set; }

        public virtual int Number { get; set; }
       
        
        public virtual MissionType Type { get; set; }


        public virtual string Country { get; set; }
        public virtual string Organization { get; set; }
        public virtual string Goal { get; set; }
        public virtual string FinancesSource { get; set; }
        public virtual string Reason { get; set; }
       

      
        public virtual TimesheetStatus TimesheetStatus { get; set; }
       

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }

        public virtual IList<MissionComment> Comments { get; set; }

        public virtual bool DeleteAfterSendTo1C { get; set; }
        
        
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}