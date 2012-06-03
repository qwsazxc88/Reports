namespace Reports.Core.Domain
{
    public class Role : AbstractUsedEntityWithVersion
    {
        public virtual string Name { get; set; }
        //public virtual IList<User> Users { get; set; }
    }
}