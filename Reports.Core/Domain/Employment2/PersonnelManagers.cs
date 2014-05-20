using System;

namespace Reports.Core.Domain
{
    public class PersonnelManagers : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual DateTime? EmploymentOrderDate { get; set; } // OK
        public virtual string EmploymentOrderNumber { get; set; } // OK
        public virtual DateTime? EmploymentDate { get; set; } // OK
        public virtual DateTime? ContractDate { get; set; } // OK
        public virtual string ContractNumber { get; set; } // OK
        public virtual decimal? NorthernAreaAddition { get; set; } // OK
        public virtual decimal? AreaMultiplier { get; set; } // OK
        public virtual decimal? AreaAddition { get; set; } // OK
        public virtual decimal? TravelRelatedAddition { get; set; } // OK
        public virtual decimal? CompetenceAddition { get; set; } // OK
        public virtual decimal? FrontOfficeExperienceAddition { get; set; }
        public virtual int? Grade { get; set; }// Грейд (is it necessary?)
        public virtual int? InsurableExperienceYears { get; set; }
        public virtual int? InsurableExperienceMonths { get; set; }
        public virtual int? InsurableExperienceDays { get; set; }
        public virtual int? NorthExperienceYears { get; set; }
        public virtual int? NorthExperienceMonths { get; set; }
        public virtual int? NorthExperienceDays { get; set; }
        // Ознакомлен с регламентными документами
        public virtual string PersonalAccount { get; set; }
        // Заявление на вычет
        public virtual decimal? PreviousIncome { get; set; }
        public virtual bool? IsNonResident { get; set; }
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