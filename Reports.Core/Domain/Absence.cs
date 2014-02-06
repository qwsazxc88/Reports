using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Неявка
    /// </summary>
    public class Absence : AbstractEntityWithVersion//AbstractEntity
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime BeginDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int DaysCount { get; set; }

        public virtual int Number { get; set; }
        //public virtual string Comment { get; set; }
        
        public virtual AbsenceType Type { get; set; }
        //public virtual RequestStatus Status { get; set; }

        //public virtual RequestStatusEnum StatusId {
        //    get { return (RequestStatusEnum)Status.Id; }
        //}
        public virtual TimesheetStatus TimesheetStatus { get; set; }
        //public virtual EmployeeDocumentType  Type { get; set; }
        //public virtual EmployeeDocumentSubType SubType { get; set; }

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual bool DeleteAfterSendTo1C { get; set; }

        public virtual IList<AbsenceComment> Comments { get; set; }
        //public virtual DateTime? BudgetManagerDateAccept { get; set; }
        //public virtual DateTime? OutsourcingManagerDateAccept { get; set; }
        //public virtual bool SendEmailToBilling { get; set; }
        //public virtual IList<DocumentComment> Comments { get; set; }
        
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}