namespace Reports.Core.Domain
{
    public class Reference : AbstractEntityWithVersion
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Patronymic { get; set; }
        public virtual string WorksAt { get; set; }
        public virtual string Position { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Relation { get; set; }
    }
}
