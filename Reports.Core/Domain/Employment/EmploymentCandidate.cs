using System;
using System.Collections.Generic;

namespace Reports.Core.Domain.Employment
{
    public class EmploymentCandidate : AbstractEntityWithVersion
    {
        // BackgroundCheck

        // ? будет заполняться после трудоустройства кандидата
        public virtual User User { get; set; }
        public virtual int? AverageSalary { get; set; }
        public virtual IList<Liability> Liabilities { get; set; }
        public virtual string DismissalReason { get; set; }
        public virtual string PreviousSuperior { get; set; }
        // ? справочник?
        public virtual string PositionSought { get; set; }
        public virtual string MilitaryOperationsExperience { get; set; }
        public virtual bool HasDriversLicense { get; set; }
        public virtual DateTime DriversLicenseDateOfIssue { get; set; }
        // битовое поле с открытыми категориями
        public virtual int DriversLicenseCategories { get; set; }
        public virtual int? DrivingExperience { get; set; }
        public virtual IList<Automobile> Automobiles { get; set; }
        public virtual bool IsReadyForBusinessTrips { get; set; }
        public virtual string Sports { get; set; }
        public virtual string Hobbies { get; set; }
        public virtual string ImportantEvents { get; set; }
        public virtual IList<Reference> References { get; set; }
        public virtual string ChronicalDiseases { get; set; }

        // Contacts

        public virtual string ZipCode { get; set; }
        public virtual string Region { get; set; }
        public virtual string District { get; set; }
        public virtual string City { get; set; }
        public virtual string Street { get; set; }
        public virtual string StreetNumber { get; set; }
        public virtual string Building { get; set; }
        public virtual string Appartment { get; set; }
        public virtual string WorkPhone { get; set; }
        public virtual string HomePhone { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Email { get; set; }

        // Education

        // TODO Заполнить все подчиненные классы
        public virtual IList<HigherEducationDiploma> HigherEducationDiplomas { get; set; }
        public virtual IList<PostGraduateEducationDiploma> PostGraduateEducationDiplomas { get; set; }
        public virtual IList<Certification> Certifications { get; set; }
        public virtual IList<Training> Training { get; set; }

        // Experience

        // TODO Заполнить ExperienceItem
        public virtual IList<ExperienceItem> ExperienceItems { get; set; }
        public virtual string ExperienceYears { get; set; }
        public virtual string ExperienceMonths { get; set; }
        public virtual string WorkBookSeries { get; set; }
        public virtual string WorkBookNumber { get; set; }
        public virtual DateTime WorkBookDateOfIssue { get; set; }
        public virtual string WorkBookSupplementSeries { get; set; }
        public virtual string WorkBookSupplementNumber { get; set; }
        public virtual DateTime WorkBookSupplementDateOfIssue { get; set; }


    }
}
