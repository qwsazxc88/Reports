using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Заявка (найм)
    /// </summary>
    public class Appointment : AbstractEntityWithVersion
    {
        #region Fields

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual bool IsStoped { get; set; }
        public virtual int Number { get; set; }
        public virtual string FIO { get; set; }
        public virtual int Recruter { get; set; }
        public virtual Department Department { get; set; }
        public virtual string City { get; set; }
        public virtual string PositionName { get; set; }
        public virtual int VacationCount { get; set; }
        public virtual AppointmentReason Reason { get; set; }
        public virtual DateTime? ReasonBeginDate { get; set; }
        public virtual string ReasonPosition { get; set; }
        //public virtual string ReasonPersonnelStore { get; set; }
        //public virtual string ReasonUser { get; set; }

        //public virtual string Period { get; set; }
        public virtual string Schedule { get; set; }
        public virtual decimal Salary { get; set; }
        public virtual decimal Bonus { get; set; }
        public virtual bool Type { get; set; }
        public virtual string Compensation { get; set; }

        public virtual string EducationRequirements { get; set; }
        public virtual string ExperienceRequirements { get; set; }
        public virtual string OtherRequirements { get; set; }
        //public virtual string AdditionalRequirements { get; set; }
        public virtual string Responsibility { get; set; }
        public virtual DateTime? DesirableBeginDate { get; set; }
        public virtual bool IsVacationExists { get; set; }
        //public virtual DateTime? BeginDate { get; set; }
        public virtual string PyrusNumber { get; set; }

        public virtual User Creator { get; set; }

        public virtual User StaffCreator { get; set; }

        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual User AcceptManager { get; set; }
        public virtual DateTime? ChiefDateAccept { get; set; }
        public virtual User AcceptChief { get; set; }
        //public virtual DateTime? PersonnelDateAccept { get; set; }
        //public virtual User AcceptPersonnel { get; set; }
        public virtual DateTime? StaffDateAccept { get; set; }
        public virtual User AcceptStaff { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual User DeleteUser { get; set; }

        public virtual DateTime? SendTo1C { get; set; }
        public virtual bool? BankAccountantAccept { get; set; }
        public virtual int BankAccountantAcceptCount { get; set; }
        //public virtual bool DeleteAfterSendTo1C { get; set; }

        //public virtual Mission Mission { get; set; }

        //public virtual IList<MissionTarget> Targets { get; set; }

        public virtual IList<AppointmentComment> Comments { get; set; }
        public virtual IList<EmploymentCandidate> Candidates { get; set; }
        public virtual IList<User> Recruters { get; set; }
        public virtual IList<AppointmentReport> Reports { get; set; }
        public virtual User BankAccountant { get; set; }

        public virtual int AppointmentEducationTypeId { get; set; }
        public virtual int Priority { get; set; }
        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}