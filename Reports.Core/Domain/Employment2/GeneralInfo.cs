using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class GeneralInfo : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual string LastName { get; set; } //ok
        public virtual string FirstName { get; set; } //ok
        public virtual string Patronymic { get; set; } //ok
        public virtual IList<NameChange> NameChanges { get; set; } //ok
        public virtual bool IsMale { get; set; } //ok
        public virtual Country Citizenship { get; set; } //ok
        public virtual InsuredPersonType InsuredPersonType { get; set; } //ok
        public virtual DateTime DateOfBirth { get; set; } //ok
        public virtual string RegionOfBirth { get; set; } //ok
        public virtual string DistrictOfBirth { get; set; } //ok
        public virtual string CityOfBirth { get; set; } //ok
        public virtual IList<ForeignLanguage> ForeignLanguages { get; set; } //ok
        public virtual string INN { get; set; } //ok
        public virtual string SNILS { get; set; } //ok
        public virtual IList<Disability> Disabilities { get; set; } //ok
        public virtual TaxpayerStatus Status { get; set; } // - статус налогоплательщика по НДФЛ
        public virtual bool AgreedToPersonalDataProcessing { get; set; } //ok
        // Фото
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