using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// ������ (����)
    /// </summary>
    public class AppointmentReport : AbstractEntityWithVersion
    {
        #region Fields

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual int Number { get; set; }
        public virtual int SecondNumber { get; set; }
        public virtual int TestingResult { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime? ColloquyDate { get; set; }
        public virtual DateTime? LessonDate { get; set; }
        public virtual AppointmentEducationType Type { get; set; }
        public virtual string EducationTime { get; set; }
        public virtual string TempLogin { get; set; }
        public virtual string TempPassword { get; set; }
        public virtual string RejectReason { get; set; }
        public virtual bool? IsEducationExists { get; set; }
        public virtual bool? IsColloquyPassed { get; set; }
        public virtual DateTime? DateAccept { get; set; }
        public virtual DateTime? CandidateRejectDate { get; set; }
        public virtual User CandidateRejectedBy { get; set; }
        public virtual string ResumeComment { get; set; }
        public virtual string ResumeCommentByOPINP { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? StaffDateAccept { get; set; }
        public virtual User AcceptStaff { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual User AcceptManager { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual User DeleteUser { get; set; }
        public virtual DateTime? SendTo1C { get; set; }

        public virtual IList<AppointmentReportComment> Comments { get; set; }
        public virtual IList<EmploymentCandidate> Candidates { get; set; }
        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}