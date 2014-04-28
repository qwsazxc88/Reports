namespace Reports.Core.Domain
{
    /// <summary>
    /// Связь руководителя уровня 2 и руководителя уровня 3 (найм)
    /// </summary>
    public class AppointmentManager2ToManager3 : AbstractEntity 
    {
        public virtual User Manager2 { get; set; }
        public virtual User Manager3 { get; set; }
    }
}
