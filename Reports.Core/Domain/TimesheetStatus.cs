namespace Reports.Core.Domain
{
    public class TimesheetStatus : AbstractEntityWithVersion
    {
        public virtual string ShortName { get; set; }
        public virtual string Name { get; set; }
    }
}