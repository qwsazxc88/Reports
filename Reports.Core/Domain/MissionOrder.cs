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
    public class MissionOrder : AbstractEntityWithVersion
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual DateTime BeginDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        
        public virtual int Number { get; set; }
       
        
        public virtual MissionType Type { get; set; }
        public virtual MissionGoal Goal { get; set; }

        public virtual decimal? SumAir { get; set; }
        public virtual decimal? SumDaily { get; set; }
        public virtual decimal? SumResidence { get; set; }
        public virtual decimal? SumTrain { get; set; }
        public virtual decimal AllSum { get; set; }
        public virtual decimal? UserSumDaily { get; set; }
        public virtual decimal? UserSumResidence { get; set; }
        public virtual decimal? UserSumAir { get; set; }
        public virtual decimal? UserSumTrain { get; set; }
        public virtual decimal UserAllSum { get; set; }
        public virtual decimal? UserSumCash { get; set; }
        public virtual decimal? UserSumNotCash { get; set; }

        public virtual bool NeedToAcceptByChief { get; set; }
        public virtual bool NeedToAcceptByChiefAsManager { get; set; }

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual User AcceptUser { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual User AcceptManager { get; set; }
        public virtual DateTime? ChiefDateAccept { get; set; }
        public virtual User AcceptChief { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual bool DeleteAfterSendTo1C { get; set; }

        public virtual Mission Mission { get; set; }

        public virtual IList<MissionTarget> Targets { get; set; }

        public virtual IList<MissionOrderComment> Comments { get; set; }

      
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}