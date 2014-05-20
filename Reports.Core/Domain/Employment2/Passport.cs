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
        public virtual DocumentType DocumentType { get; set; } // OK
        public virtual string InternalPassportSeries { get; set; } // OK
        public virtual string InternalPassportNumber { get; set; } // OK
        public virtual DateTime? InternalPassportDateOfIssue { get; set; } // OK
        public virtual string InternalPassportIssuedBy { get; set; } // OK
        public virtual string InternalPassportSubdivisionCode { get; set; } // OK
        public virtual DateTime? RegistrationDate { get; set; } // OK
        public virtual string ZipCode { get; set; } // OK
        public virtual string Region { get; set; } // OK
        public virtual string District { get; set; } // OK
        public virtual string City { get; set; } // OK
        public virtual string Street { get; set; } // OK
        public virtual string StreetNumber { get; set; } // OK
        public virtual string Building { get; set; } // OK
        public virtual string Appartment { get; set; } // OK
        public virtual string InternationalPassportSeries { get; set; } // OK
        public virtual string InternationalPassportNumber { get; set; } // OK
        public virtual DateTime? InternationalPassportDateOfIssue { get; set; } // OK
        public virtual string InternationalPassportIssuedBy { get; set; } // OK
        // Скан паспорта
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