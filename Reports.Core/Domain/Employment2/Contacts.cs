namespace Reports.Core.Domain
{
    public class Contacts : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual string ZipCode { get; set; } //ok
        public virtual string Region { get; set; } //ok
        public virtual string District { get; set; } //ok
        public virtual string City { get; set; } //ok
        public virtual string Street { get; set; } //ok
        public virtual string StreetNumber { get; set; } //ok
        public virtual string Building { get; set; } //ok
        public virtual string Apartment { get; set; } //ok
        public virtual string WorkPhone { get; set; } //ok
        public virtual string HomePhone { get; set; } //ok
        public virtual string Mobile { get; set; } //ok
        public virtual string Email { get; set; } //ok
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