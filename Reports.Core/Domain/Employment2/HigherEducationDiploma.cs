using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class HigherEducationDiploma : AbstractEntity
    {
        public virtual User User { get; set; }
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
