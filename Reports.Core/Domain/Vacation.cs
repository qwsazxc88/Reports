using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Patient
    /// </summary>
    public class Vacation : AbstractEntityWithVersion//AbstractEntity
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime BeginDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int DaysCount { get; set; }

        public virtual int Number { get; set; }
        //public virtual string Comment { get; set; }
        
        public virtual VacationType Type { get; set; }
        public virtual RequestStatus Status { get; set; }
        public virtual TimesheetStatus TimesheetStatus { get; set; }
        //public virtual EmployeeDocumentType  Type { get; set; }
        //public virtual EmployeeDocumentSubType SubType { get; set; }

        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual DateTime? PersonnelManagerDateAccept { get; set; }

        public virtual IList<VacationComment> Comments { get; set; }
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