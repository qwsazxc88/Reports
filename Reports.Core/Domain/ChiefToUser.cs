namespace Reports.Core.Domain
{
    public class ChiefToUser : AbstractEntity 
    {
        public virtual User Chief { get; set; }
        public virtual User User { get; set; }
    }
}
