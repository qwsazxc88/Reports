using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class BackgroundCheck : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual decimal? AverageSalary { get; set; } //ok
        public virtual string Liabilities { get; set; } //ok
        public virtual string PreviousDismissalReason { get; set; } //ok
        public virtual string PreviousSuperior { get; set; } //ok
        public virtual string PositionSought { get; set; } //ok
        public virtual string MilitaryOperationsExperience { get; set; } //ok
        public virtual string DriversLicenseNumber { get; set; } //ok
        public virtual DateTime? DriversLicenseDateOfIssue { get; set; } //ok
        public virtual int? DriversLicenseCategories { get; set; } //ok - битовое поле
        public virtual int? DrivingExperience { get; set; } //ok
        public virtual string AutomobileMake { get; set; } //ok
        public virtual string AutomobileLicensePlateNumber { get; set; } //ok
        public virtual bool IsReadyForBusinessTrips { get; set; } //ok
        public virtual string Sports { get; set; } //ok
        public virtual string Hobbies { get; set; } //ok
        public virtual string ImportantEvents { get; set; } //ok
        public virtual IList<Reference> References { get; set; } //ok
        public virtual string ChronicalDiseases { get; set; } //ok
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        #endregion
    }
}