namespace Reports.Core.Domain
{
    public class Role : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        //public virtual IList<User> Users { get; set; }
    }
}