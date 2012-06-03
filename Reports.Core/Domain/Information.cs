namespace Reports.Core.Domain
{
    public class Information : AbstractEntityWithVersion
    {
        public virtual string Subject { get; set; }
        public virtual string Message { get; set; }
    }
}