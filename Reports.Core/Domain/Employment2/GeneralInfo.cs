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
        public virtual string LastName { get; set; } // OK
        public virtual string FirstName { get; set; } // OK
        public virtual string Patronymic { get; set; } // OK
        public virtual IList<NameChange> NameChanges { get; set; } // OK/?
        public virtual bool IsMale { get; set; } // OK
        public virtual Country Citizenship { get; set; } // OK
        public virtual InsuredPersonType InsuredPersonType { get; set; } // OK
        public virtual DateTime? DateOfBirth { get; set; } // OK
        public virtual string RegionOfBirth { get; set; } // OK
        public virtual string DistrictOfBirth { get; set; } // OK
        public virtual string CityOfBirth { get; set; } // OK
        public virtual IList<ForeignLanguage> ForeignLanguages { get; set; } // OK?
        public virtual string INN { get; set; } // OK
        public virtual string SNILS { get; set; } // OK
        public virtual IList<Disability> Disabilities { get; set; } // OK?
        public virtual TaxpayerStatus Status { get; set; } // OK - статус налогоплательщика по НДФЛ
        public virtual bool? AgreedToPersonalDataProcessing { get; set; } // OK
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