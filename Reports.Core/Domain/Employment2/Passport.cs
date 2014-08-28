using System;

namespace Reports.Core.Domain
{
    public class Passport : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual DocumentType DocumentType { get; set; } //ok
        public virtual string InternalPassportSeries { get; set; } //ok
        public virtual string InternalPassportNumber { get; set; } //ok
        public virtual DateTime? InternalPassportDateOfIssue { get; set; } //ok
        public virtual string InternalPassportIssuedBy { get; set; } //ok
        public virtual string InternalPassportSubdivisionCode { get; set; } //ok
        public virtual DateTime? RegistrationDate { get; set; } //ok
        public virtual string ZipCode { get; set; } //
        public virtual string Region { get; set; } //
        public virtual string District { get; set; } //
        public virtual string City { get; set; } //
        public virtual string Street { get; set; } //
        public virtual string StreetNumber { get; set; } //
        public virtual string Building { get; set; } //
        public virtual string Apartment { get; set; } //
        public virtual string InternationalPassportSeries { get; set; } //
        public virtual string InternationalPassportNumber { get; set; } //
        public virtual DateTime? InternationalPassportDateOfIssue { get; set; } //
        public virtual string InternationalPassportIssuedBy { get; set; } //
        // Скан паспорта

        public virtual bool IsFinal { get; set; }
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