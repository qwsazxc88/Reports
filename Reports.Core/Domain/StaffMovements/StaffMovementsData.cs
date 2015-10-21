using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class StaffMovementsData: AbstractEntity
    {
        public virtual decimal AdditionPersonnel { get; set; }
        public virtual int AdditionPersonnelAction { get; set; }
        public virtual DateTime? AdditionPersonnelTo { get; set; }
        public virtual decimal AdditionTerritory { get; set; }
        public virtual int AdditionTerritoryAction { get; set; }
        public virtual decimal AdditionPosition { get; set; }
        public virtual int AdditionPositionAction { get; set; }
        public virtual DateTime? AdditionPositionTo { get; set; }
        public virtual decimal AdditionQuality { get; set; }
        public virtual int AdditionQualityAction { get; set; }
        public virtual DateTime? AdditionQualityTo { get; set; }
        public virtual decimal AdditionTraveling { get; set; }
        public virtual int AdditionTravelingAction { get; set; }
        public virtual DateTime? AdditionTravelingTo { get; set; }
        public virtual decimal AdditionFront { get; set; }
        public virtual int AdditionFrontAction { get; set; }
        public virtual DateTime? AdditionFrontTo { get; set; }
        public virtual decimal TargetCasing { get; set; }
        public virtual string AdditionalAgreementNumber { get; set; }
        public virtual DateTime? AdditionalAgreementDate { get; set; }
        public virtual int SalaryType { get; set; }
        public virtual decimal Salary { get; set; }
        public virtual decimal RegionCoefficient { get; set; }
        public virtual int Grade { get; set; }
        public virtual Schedule HoursType { get; set; }
        public virtual int NorthFactor { get; set; }
        public virtual int NorthFactorOrder { get; set; }
        public virtual int NorthFactorYear { get; set; }
        public virtual int NorthFactorMonth { get; set; }
        public virtual int NorthFactorDay { get; set; }
        public virtual decimal NorthFactorAddition { get; set; }
        public virtual int NorthFactorAdditionAction { get; set; }
        public virtual string ChangesToAgreementEntries { get; set; }
        public virtual string ChangesToAgreement { get; set; }
        public virtual DateTime? AgreementDate { get; set; }
        public virtual string AdditionalAgreementEntries { get; set; }
        public virtual AccessGroup AccessGroup { get; set; }
        public virtual string MovementReason { get; set; }
        public virtual string MovementReasonOrder { get; set; }
        public virtual string MovementTempReason { get; set; }
        public virtual string MovementCondition { get; set; }
        public  virtual Signer Signatory { get; set; }
        public virtual bool Conjunction { get; set; }
    }
}
