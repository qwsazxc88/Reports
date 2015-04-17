using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class PersonnelManagers : AbstractEntityWithVersion, IEmploymentInfoSection
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual DateTime? EmploymentOrderDate { get; set; } //ok
        public virtual string EmploymentOrderNumber { get; set; } //ok
        public virtual DateTime? EmploymentDate { get; set; } //ok
        public virtual DateTime? ContractDate { get; set; } //ok
        public virtual DateTime? ContractEndDate { get; set; }
        public virtual IList<SupplementaryAgreement> SupplementaryAgreements { get; set; }
        public virtual string ContractNumber { get; set; } //ok
        public virtual bool IsHourlySalaryBasis { get; set; }
        public virtual decimal? BasicSalary { get; set; } //ok
        public virtual decimal? NorthernAreaAddition { get; set; } //ok
        public virtual decimal? AreaMultiplier { get; set; } //ok
        public virtual decimal? AreaAddition { get; set; } //ok
        public virtual decimal? TravelRelatedAddition { get; set; } //ok
        public virtual decimal? CompetenceAddition { get; set; } //ok
        public virtual decimal? FrontOfficeExperienceAddition { get; set; }
        public virtual int OverallExperienceYears { get; set; } //ok
        public virtual int OverallExperienceMonths { get; set; } //ok
        public virtual int OverallExperienceDays { get; set; } //ok
        public virtual int InsurableExperienceYears { get; set; } //ok
        public virtual int InsurableExperienceMonths { get; set; } //ok
        public virtual int InsurableExperienceDays { get; set; } //ok
        public virtual decimal? PersonalAddition { get; set; } //ok
        public virtual decimal? PositionAddition { get; set; } //ok
        // TODO: EMPL Ознакомлен с регламентными документами
        public virtual string PersonalAccount { get; set; } //ok
        public virtual PersonalAccountContractor PersonalAccountContractor { get; set; } //ok
        // TODO: EMPL Признаки ЦФО 1
        // TODO: EMPL Признаки ЦФО 2
        // TODO: EMPL Заявление на вычет
        // TODO: EMPL Скан заявления на вычет
        public virtual AccessGroup AccessGroup { get; set; }
        public virtual Signer Signer { get; set; }
        public virtual User ApprovedByPersonnelManager { get; set; } //ok

        public virtual InsuredPersonType InsuredPersonType { get; set; } //ok
        public virtual int? Status { get; set; } // - статус налогоплательщика по НДФЛ

        public virtual Schedule Schedule { get; set; } // OK?

        public virtual DateTime? CompleteDate { get; set; } //ok

        public virtual DateTime? RejectDate { get; set; } //дата отклонения
        public virtual User RejectUser { get; set; } //сотрудник проводящий отклонение кандидат

        //дополнения для трудового договора
        public virtual int? ContractPoint_1_Id { get; set; }
        public virtual int? ContractPoint_2_Id { get; set; }
        public virtual int? ContractPoint_3_Id { get; set; }
        public virtual string ContractPointsFio { get; set; }
        public virtual string ContractPointsAddress { get; set; }
        #endregion

        #region Constructors

        public PersonnelManagers()
        {
            SupplementaryAgreements = new List<SupplementaryAgreement>();
        }

        #endregion

        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        #endregion
    }
}