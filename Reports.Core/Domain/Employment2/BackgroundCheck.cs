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
        public virtual decimal? AverageSalary { get; set; } // OK
        public virtual IList<FinancialLiability> Liabilities { get; set; } // OK-
        public virtual string PreviousDismissalReason { get; set; } // OK
        public virtual string PreviousSuperior { get; set; } // OK
        public virtual string PositionSought { get; set; } // OK
        public virtual string MilitaryOperationsExperience { get; set; } // OK
        public virtual DateTime? DriversLicenseDateOfIssue { get; set; } // OK
        public virtual int? DriversLicenseCategories { get; set; } // OK - битовое поле
        public virtual int? DrivingExperience { get; set; } // OK
        public virtual IList<Automobile> Automobiles { get; set; } // OK-
        public virtual bool? IsReadyForBusinessTrips { get; set; } // OK
        public virtual string Sports { get; set; } // OK
        public virtual string Hobbies { get; set; } // OK
        public virtual string ImportantEvents { get; set; } // OK
        public virtual IList<Reference> References { get; set; } // OK-
        public virtual string ChronicalDiseases { get; set; } // OK
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