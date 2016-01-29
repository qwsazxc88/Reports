using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Enum;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Domain
{
    public class EmploymentCandidate : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual GeneralInfo GeneralInfo { get; set; }
        public virtual Passport Passport { get; set; }
        public virtual Education Education { get; set; }
        public virtual Family Family { get; set; }
        public virtual MilitaryService MilitaryService { get; set; }
        public virtual Experience Experience { get; set; }
        public virtual Contacts Contacts { get; set; }
        public virtual BackgroundCheck BackgroundCheck { get; set; }
        public virtual OnsiteTraining OnsiteTraining { get; set; }
        public virtual Managers Managers { get; set; }
        public virtual PersonnelManagers PersonnelManagers { get; set; }
        public virtual EmploymentStatus Status { get; set; }
        public virtual DateTime? QuestionnaireDate { get; set; }
        public virtual User AppointmentCreator { get; set; }
        public virtual User Personnels { get; set; }
        public virtual bool IsTrainingNeeded { get; set; }
        public virtual bool IsBeforEmployment { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual AppointmentReport AppointmentReport { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual bool IsTechDissmiss { get; set; }
        public virtual bool IsScanFinal { get; set; }
        //для фиксирования рассылки
        public virtual bool IsCandidateToBackgroundPrevSendEmail { get; set; }
        public virtual DateTime? CandidateToBackgroundPrevSendEmailDate { get; set; }
        public virtual bool IsCandidateToBackgroundSendEmail { get; set; }
        public virtual DateTime? CandidateToBackgroundSendEmailDate { get; set; }
        public virtual bool IsCandidateToManagerSendEmail { get; set; }
        public virtual DateTime? CandidateToManagerSendEmailDate { get; set; }
        public virtual bool IsBackgroundToManagerSendEmail { get; set; }
        public virtual DateTime? BackgroundToManagerSendEmailDate { get; set; }
        public virtual bool IsManagerToTrainingSendEmail { get; set; }
        public virtual DateTime? ManagerToTrainingSendEmailDate { get; set; }
        public virtual bool IsManagerToHigherManagerSendEmail { get; set; }
        public virtual DateTime? ManagerToHigherManagerSendEmailDate { get; set; }
        public virtual bool IsPersonnelManagerToManagerSendEmail { get; set; }
        public virtual DateTime? PersonnelManagerToManagerSendEmailDate { get; set; }
        //отметки о получении оригинала ТК
        public virtual bool IsTKReceived { get; set; }
        public virtual DateTime? TKReceivedDate { get; set; }
        public virtual User TKMarkUser { get; set; }
        //отметки о получении оригинала ТД
        public virtual bool IsTDReceived { get; set; }
        public virtual DateTime? TDReceivedDate { get; set; }
        public virtual User TDMarkUser { get; set; }
        public virtual string PyrusNumber { get; set; }
        public virtual int? UserLinkId { get; set; }
    }
}