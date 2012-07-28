namespace Reports.Core.Domain
{
    public class RequestNextNumber : AbstractEntityWithVersion
    {
        public virtual int RequestTypeId { get; set; }
        public virtual int NextNumber { get; set; }
        //public virtual IList<User> Users { get; set; }
    }
}