namespace Reports.Core.Domain
{
    public class Contacts : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
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