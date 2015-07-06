using System;
using System.Collections.Generic;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Заявка на создание/изменение/удаление подразделений.
    /// </summary>
    public class StaffDepartmentRequest : AbstractEntityWithVersion
    {
        public virtual DateTime? DateRequest { get; set; }
        public virtual StaffDepartmentRequestTypes RequestType { get; set; }
        public virtual Department Department { get; set; }
        public virtual int ItemLevel { get; set; }
        public virtual Department ParentDepartment { get; set; }
        public virtual string Name { get; set; } 
        public virtual bool IsBack { get; set; } 
        public virtual string OrderNumber { get; set; }
        public virtual DateTime? OrderDate { get; set; }
        public virtual RefAddresses LegalAddress { get; set; } 
        public virtual bool IsTaxAdminAccount { get; set; } 
        public virtual bool IsEmployeAvailable { get; set; }
        public virtual Department DepNext { get; set; }
        public virtual bool IsPlan { get; set; } 
        public virtual bool IsUsed { get; set; } 
        public virtual bool IsDraft { get; set; } 
        public virtual DateTime? DateSendToApprove { get; set; } 
        public virtual DateTime? BeginAccountDate { get; set; } 
        public virtual DateTime? DateState { get; set; } 
        public virtual User Creator { get; set; } 
        public virtual DateTime? CreateDate { get; set; } 
        public virtual User Editor { get; set; } 
        public virtual DateTime? EditDate { get; set; }
        public virtual IList<StaffDepartmentCBDetails> DepartmentCBDetails { get; set; }
        public virtual IList<StaffDepartmentManagerDetails> DepartmentManagerDetails { get; set; }
    }
}
