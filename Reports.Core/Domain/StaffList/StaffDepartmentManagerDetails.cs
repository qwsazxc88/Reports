using System;
using System.Collections.Generic;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Управленческие реквизиты заявки для подразделения
    /// </summary>
    public class StaffDepartmentManagerDetails : AbstractEntityWithVersion
    {
        public virtual StaffDepartmentRequest DepRequest { get; set; }
        public virtual string DepCode { get; set; }
        public virtual string NameShort { get; set; }
        public virtual string NameComment { get; set; }
        public virtual StaffDepartmentReasons DepartmentReasons { get; set; }
        public virtual string PrevDepCode { get; set; }
        public virtual RefAddresses FactAddress { get; set; }
        public virtual string DepStatus { get; set; }
        public virtual StaffDepartmentTypes DepartmentType { get; set; }
        public virtual DateTime? OpenDate { get; set; }
        public virtual DateTime? CloseDate { get; set; }
        public virtual string OperationMode { get; set; }
        public virtual DateTime? BeginIdleDate { get; set; }
        public virtual DateTime? EndIdleDate { get; set; }
        public virtual bool IsRentPlace { get; set; }
        public virtual string AgreementDetails { get; set; }
        public virtual decimal? DivisionArea { get; set; }
        public virtual decimal? AmountPayment { get; set; }
        public virtual string Phone { get; set; }
        public virtual bool IsBlocked { get; set; }
        public virtual bool IsNetShop { get; set; }
        public virtual bool IsAvailableCash { get; set; }
        public virtual bool IsLegalEntity { get; set; }
        public virtual int PlanEPCount { get; set; }
        public virtual decimal PlanSalaryFund { get; set; }
        public virtual string Note { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual IList<StaffDepartmentOperationModes> DepOperationModes { get; set; }
        public virtual IList<StaffDepartmentOperationLinks> DepOperations { get; set; }
        public virtual IList<StaffProgramCodes> ProgramCodes { get; set; }
        public virtual IList<StaffDepartmentLandmarks> DepartmentLandmarks { get; set; }
    }
}
