using System;

namespace Reports.Core.Domain
{
    public class MilitaryService : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual bool IsLiableForMilitaryService { get; set; } //ok
        public virtual string MilitaryCardNumber { get; set; } //ok
        public virtual DateTime? MilitaryCardDate { get; set; } //ok
        public virtual string ReserveCategory { get; set; } //ok
        public virtual int? Rank { get; set; } //ok
        public virtual string SpecialityCategory { get; set; } //ok - Состав (профиль)
        public virtual string MilitarySpecialityCode { get; set; } //ok
        public virtual string CombatFitness { get; set; } //ok
        public virtual string Commissariat { get; set; } //ok
        public virtual string MilitaryServiceRegistrationInfo { get; set; } //ok
        public virtual string CommonMilitaryServiceRegistrationInfo { get; set; }
        public virtual string SpecialMilitaryServiceRegistrationInfo { get; set; }
        public virtual int? RegistrationExpiration { get; set; } //ok
        public virtual bool IsReserved { get; set; }
        public virtual string MobilizationTicketNumber { get; set; }
        public virtual int? PersonnelCategory { get; set; } //ok
        public virtual int? PersonnelType { get; set; } //ok
        public virtual bool IsAssigned { get; set; } //ok
        public virtual int? ConscriptionStatus { get; set; } //ok
        // Скан военного билета
        // Скан мобилизационного талона

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