namespace Reports.Core.Domain
{
    public class PostGraduateEducationDiploma : AbstractEntityWithVersion
    {
        public virtual string IssuedBy { get; set; }
        public virtual string Series { get; set; }
        public virtual string Number { get; set; }
        public virtual string AdmissionYear { get; set; }
        public virtual string GraduationYear { get; set; }
        public virtual string Speciality { get; set; }
    }
}
