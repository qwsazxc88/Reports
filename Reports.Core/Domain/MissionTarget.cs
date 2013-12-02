using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class MissionTarget : AbstractEntityWithVersion //AbstractEntity
    {
        #region Fields

        public virtual MissionOrder MissionOrder { get; set; }
        public virtual MissionCountry Country { get; set; }
        public virtual string City { get; set; }
        public virtual string Organization { get; set; }

        public virtual DateTime BeginDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int DaysCount { get; set; }
        public virtual int RealDaysCount { get; set; }

        public virtual MissionDailyAllowance DailyAllowance { get; set; }
        public virtual MissionResidence Residence { get; set; }
        public virtual MissionAirTicketType AirTicketType { get; set; }
        public virtual MissionTrainTicketType TrainTicketType { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}