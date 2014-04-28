namespace Reports.Core.Domain
{
    /// <summary>
    /// Связь руководителя уровня 2 с департаментом уровня 2,"недосвязь", только создание заявок (найм)
    /// </summary>
    public class AppointmentCreateManager2ToDepartment2 : AbstractEntity 
    {
        public virtual User Manager { get; set; }
        public virtual Department Department { get; set; }
    }
}
