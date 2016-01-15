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
        public virtual StaffDepartmentAccessory DepartmentAccessory { get; set; }
        public virtual string OrderNumber { get; set; }
        public virtual DateTime? OrderDate { get; set; }
        public virtual RefAddresses LegalAddress { get; set; } 
        public virtual bool IsTaxAdminAccount { get; set; } 
        public virtual bool IsEmployeAvailable { get; set; }
        /// <summary>
        /// Подразделение с налоговыми реквизитами (сосед)
        /// </summary>
        public virtual Department DepNext { get; set; }
        /// <summary>
        /// Депозитное подразделение.
        /// </summary>
        public virtual Department DepDeposit { get; set; }
        public virtual bool IsPlan { get; set; } 
        public virtual bool IsUsed { get; set; } 
        public virtual bool IsDraft { get; set; }
        public virtual bool IsTaxRequest { get; set; } 
        public virtual DateTime? DateSendToApprove { get; set; } 
        public virtual DateTime? BeginAccountDate { get; set; } 
        public virtual DateTime? DateState { get; set; }
        public virtual DateTime? DeleteDate { get; set; } 
        public virtual User Creator { get; set; } 
        public virtual DateTime? CreateDate { get; set; } 
        public virtual User Editor { get; set; } 
        public virtual DateTime? EditDate { get; set; }
        /// <summary>
        /// ЦБ реквизиты подразделения
        /// </summary>
        public virtual IList<StaffDepartmentCBDetails> DepartmentCBDetails { get; set; }
        /// <summary>
        /// Управленческие реквизиты
        /// </summary>
        public virtual IList<StaffDepartmentManagerDetails> DepartmentManagerDetails { get; set; }
        /// <summary>
        /// Задачи в Пайрусе
        /// </summary>
        public virtual IList<StaffRequestPyrusTasks> StaffRequestPyrusTasks { get; set; }
    }
}
