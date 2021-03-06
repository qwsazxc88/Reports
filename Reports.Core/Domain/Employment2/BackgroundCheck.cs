using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class BackgroundCheck : AbstractEntityWithVersion, IEmploymentInfoSection
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
        public virtual bool HasDriversLicense { get; set; }
        public virtual string DriversLicenseNumber { get; set; } //ok
        public virtual DateTime? DriversLicenseDateOfIssue { get; set; } //ok
        public virtual string DriversLicenseCategories { get; set; } //ok - битовое поле
        public virtual int? DrivingExperience { get; set; } //ok
        public virtual bool HasAutomobile { get; set; }
        public virtual string AutomobileMake { get; set; } //ok
        public virtual string AutomobileLicensePlateNumber { get; set; } //ok
        public virtual bool IsReadyForBusinessTrips { get; set; } //ok
        public virtual string Sports { get; set; } //ok
        public virtual string Hobbies { get; set; } //ok
        public virtual string ImportantEvents { get; set; } //ok
        public virtual IList<Reference> References { get; set; } //ok
        public virtual string ChronicalDiseases { get; set; } //ok
        public virtual string Penalties { get; set; } //ok
        public virtual string PsychiatricAndAddictionTreatment { get; set; }
        public virtual string Smoking { get; set; }
        public virtual string Drinking { get; set; }
        public virtual string PyrusRef { get; set; } //ok
        public virtual string OwnerOfShares { get; set; }
        public virtual string PositionInGoverningBodies { get; set; } //ok

        public virtual bool IsApprovalSkipped { get; set; }
        public virtual bool? ApprovalStatus { get; set; }
        public virtual User Approver { get; set; }
        public virtual DateTime? ApprovalDate { get; set; } //ok
        //предварительное согласование
        public virtual bool? PrevApprovalStatus { get; set; }
        public virtual User PrevApprover { get; set; }
        public virtual DateTime? PrevApprovalDate { get; set; } //ok

        public virtual User CancelRejectUser { get; set; }
        public virtual DateTime? CancelRejectDate { get; set; } //ok

        public virtual bool IsFinal { get; set; }
        public virtual bool IsValidate { get; set; }
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