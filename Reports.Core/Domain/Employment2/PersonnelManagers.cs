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
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual DateTime? EmploymentOrderDate { get; set; } //ok
        public virtual string EmploymentOrderNumber { get; set; } //ok
        public virtual DateTime? EmploymentDate { get; set; } //ok
        public virtual DateTime? ContractDate { get; set; } //ok
        public virtual string ContractNumber { get; set; } //ok
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
        // TODO: EMPL ���������� � ������������� �����������
        public virtual string PersonalAccount { get; set; } //ok
        public virtual string PersonalAccountContractor { get; set; } //ok
        // TODO: EMPL �������� ��� 1
        // TODO: EMPL �������� ��� 2
        // TODO: EMPL ��������� �� �����
        // TODO: EMPL ���� ��������� �� �����
        public virtual AccessGroup AccessGroup { get; set; }
        public virtual User ApprovedByPersonnelManager { get; set; } //ok
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