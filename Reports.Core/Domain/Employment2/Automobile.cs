namespace Reports.Core.Domain
{
    public class Automobile : AbstractEntityWithVersion
    {
        public virtual string Make { get; set; }
        public virtual string LicensePlateNumber { get; set; }
    }
}