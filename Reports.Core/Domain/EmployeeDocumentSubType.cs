namespace Reports.Core.Domain
{
    public class EmployeeDocumentSubType : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual EmployeeDocumentType Type { get; set; }
        //public virtual IList<User> Users { get; set; }
    }
}