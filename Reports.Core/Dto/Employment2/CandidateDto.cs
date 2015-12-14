using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class CandidateDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ContractNumber1C { get; set; }

        public string Name { get; set; }

        public string WorkCity { get; set; }

        public string Department { get; set; }

        public string Department3 { get; set; }

        public string Position { get; set; }

        public DateTime? EmploymentDate { get; set; }

        public string EmploymentOrderNumber { get; set; }

        public DateTime? EmploymentOrderDate { get; set; }

        public string ContractNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public DateTime? QuestionnaireDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public DateTime? SupplementaryAgreementCreateDate { get; set; }

        public int? SupplementaryAgreementNumber { get; set; }

        public DateTime? IndefiniteContractOrderCreateDate { get; set; }

        public int? IndefiniteContractOrderNumber { get; set; }

        public string AppointmentManager { get; set; }
        public string PersonnelName { get; set; }
        public string Training { get; set; }

        public bool? IsFixedTermContract { get; set; }

        public bool IsSecondaryJob { get; set; }

        //public bool IsFulltime { get; set; }

        //public bool IsTemporary { get; set; }

        public string ProbationaryPeriod { get; set; }

        public string Schedule { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Disabilities { get; set; }

        public Int32 Grade { get; set; }

        public string Status { get; set; }

        public string DocStatus { get; set; }


        public bool IsChangeContractToIndefiniteAvailable { get; set; }

        public bool IsApprovedByManager { get; set; }

        public bool IsApprovedByHigherManager { get; set; }

        public bool IsApproveByManagerAvailable { get; set; }

        public bool IsApproveByHigherManagerAvailable { get; set; }

        public bool IsContractChangedToIndefinite { get; set; }

        public int AppointmentReportId { get; set; }
        public string AppointmentReportNumber { get; set; }
        public int AppointmentId { get; set; }
        public int? AppointmentNumber { get; set; }
        public bool IsTechDissmiss { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? DismissalDate { get; set; }
        public string MentorName { get; set; }
        public DateTime? PlanRegistrationDate { get; set; }

        public bool IsTKReceived { get; set; }
        public DateTime? TKReceivedDate { get; set; }
        public bool IsTDReceived { get; set; }
        public DateTime? TDReceivedDate { get; set; }
        public string AdditionAvailable { get; set; }

        /*
        [Display(Name = "Паспортные данные"),
            StringLength(500, ErrorMessage = "Не более 500 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string PassportData { get; set; }

        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Место рождения"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Место работы, должность, (рабочий, мобильный телефон)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string WorksAt { get; set; }

        [Display(Name = "Адрес места жительства, (домашний, мобильный телефон)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string WorksAt { get; set; }
        */
    }
    /// <summary>
    /// Состояние кандидата
    /// </summary>
    public class CandidateStateDto
    {
        public int Id { get; set; }
        public bool ScanFinal { get; set; }
        public bool GeneralFinal { get; set; }
        public bool PassportFinal { get; set; }
        public bool EducationFinal { get; set; }
        public bool FamilyFinal { get; set; }
        public bool MilitaryFinal { get; set; }
        public bool ExperienceFinal { get; set; }
        public bool ContactFinal { get; set; }
        public bool BackgroundFinal { get; set; }
        public bool CandidateApp { get; set; }
        public bool CandidateReady { get; set; }
        public bool? BackgroundApproval { get; set; }
        public bool? PrevBackgroundApproval { get; set; }
        public bool? TrainingApproval { get; set; }
        public bool? ManagerApproval { get; set; }
        public bool? PersonnelManagerApproval { get; set; }
    }
    /// <summary>
    /// Список кадровиков
    /// </summary>
    public class CandidatePersonnelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CandidateDocRecievedDto
    {
        public int Id { get; set; }
        public bool IsTKReceived { get; set; }
        public bool IsTDReceived { get; set; }
        public string ReceivedDate { get; set; }
    }
}
