namespace Reports.Core.Domain
{
    public class TimesheetDay : AbstractEntityWithVersion
    {
        public virtual int Number { get; set; }
        public virtual float Hours { get; set; }
        public virtual TimesheetStatus Status { get; set; }
        public virtual Timesheet Timesheet { get; set; }
    }
}