using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class GeneralInfo : AbstractEntityWithVersion, IEmploymentInfoSection
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual string LastName { get; set; } //ok
        public virtual string FirstName { get; set; } //ok
        public virtual string Patronymic { get; set; } //ok
        public virtual bool IsPatronymicAbsent { get; set; }
        public virtual IList<NameChange> NameChanges { get; set; } //ok
        public virtual bool IsMale { get; set; } //ok
        public virtual Country Citizenship { get; set; } //ok
        public virtual DateTime? DateOfBirth { get; set; } //ok
        public virtual string RegionOfBirth { get; set; } //ok
        public virtual string DistrictOfBirth { get; set; } //ok
        public virtual string CityOfBirth { get; set; } //ok
        public virtual IList<ForeignLanguage> ForeignLanguages { get; set; } //ok
        public virtual string INN { get; set; } //ok
        public virtual string SNILS { get; set; } //ok

        public virtual string DisabilityCertificateSeries { get; set; }
        public virtual string DisabilityCertificateNumber { get; set; }
        public virtual DateTime? DisabilityCertificateDateOfIssue { get; set; }
        public virtual DisabilityDegree DisabilityDegree { get; set; }
        public virtual DateTime? DisabilityCertificateExpirationDate { get; set; }
        public virtual bool IsDisabilityTermLess { get; set; }
        public virtual Country CountryBirth { get; set; }

        public virtual bool AgreedToPersonalDataProcessing { get; set; } //ok

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