namespace Reports.Core.Domain
{
    public class HigherEducationDiploma : AbstractEntityWithVersion
    {
        public virtual string IssuedBy { get; set; }
        public virtual string Series { get; set; }
        public virtual string Number { get; set; }
        public virtual string AdmissionYear { get; set; }
        public virtual string GraduationYear { get; set; }
        public virtual string Qualification { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string Profession { get; set; }
        public virtual string Department { get; set; }
    }
}
