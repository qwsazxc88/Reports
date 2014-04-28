namespace Reports.Core.Domain
{
    /// <summary>
    /// Связь руководителя уровня 2 или 3 с департаментом уровня 3 (найм)
    /// </summary>
    public class AppointmentManager23ToDepartment3 : AbstractEntity 
    {
        public virtual User Manager { get; set; }
        public virtual Department Department { get; set; }
    }
}
