using System;
using System.Collections.Generic;

namespace Reports.Core.Domain.Employment2
{
    public class BackgroundCheck : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual int? AverageSalary { get; set; }
        public virtual IList<string> Liabilities { get; set; }
        public virtual string DismissalReason { get; set; }
        public virtual string PreviousSuperior { get; set; }
        public virtual string PositionSought { get; set; }
        public virtual string MilitaryOperationsExperience { get; set; }
        public virtual bool HasDriversLicense { get; set; }
        public virtual DateTime DriversLicenseDateOfIssue { get; set; }
        // битовое поле
        public int DriversLicenseCategories { get; set; }
        public int? DrivingExperience { get; set; }

    }
}
