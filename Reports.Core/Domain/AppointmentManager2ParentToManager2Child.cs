namespace Reports.Core.Domain
{
    /// <summary>
    /// Связь руководителя уровня 2 (родитель) и руководителя уровня 2 (потомок) (найм)
    /// </summary>
    public class AppointmentManager2ParentToManager2Child : AbstractEntity 
    {
        public virtual User Parent { get; set; }
        public virtual User Child { get; set; }
    }
}
