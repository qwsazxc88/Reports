using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Dto.Employment2;
namespace Reports.Presenters.UI.ViewModel
{
    public class AppointmentEditModel : ManagerInfoModel, IContainId
    {
        public int Version { get; set; }

        #region IContainId Members
        public int Id { get; set; }
        public int UserId { get; set; }
        #endregion
        [Display(Name = "В структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool NonActual { get; set; }
        public bool IsNonActualButtonAvailable { get; set; }
        public bool isNeedToNotify { get; set; }
        public int DepartmentRequiredLevel { get; set; }
        public int Recruter { get; set; }
        public int StaffBossId { get; set; }
        public string FiredFIO { get; set; }
        [Display(Name = "Место работы (город)")]
        //[Required(ErrorMessageResourceName = "AppointmentEditModel_City_Required",ErrorMessageResourceType = typeof(Resources))]
        //[LocalizationDisplayName("AppointmentEditModel_City_Required", typeof(Resources))]
        public string City { get; set; }

        public int Priority { get; set; }
        public IList<IdNameDto> Priorities { get; set; }
        [Display(Name = "Должность")]
        public string PositionName { get; set; }
        //public int PositionId { get; set; }
        //public int PositionIdHidden { get; set; }
        //public IList<IdNameDto> Positions;
        public string FIO { get; set; }
        [Display(Name="Номер задачи в Pyrus:")]
        public string PyrusNumber { get; set; }
        [Display(Name = "Количество требуемых сотрудников")]
        public string VacationCount { get; set; }

        [Display(Name = "Основание появления вакансии")]
        public int ReasonId { get; set; }
        public int ReasonIdHidden { get; set; }
        public IList<IdNameDto> Reasons;
        public IList<IdNameDto> UsersList { get; set; }
        public int ReasonPositionId { get; set; }
        //[Display(Name = "Должность")]
        public string ReasonPosition { get; set; }
        //[Display(Name = "С даты")]
        public string ReasonBeginDate { get; set; }

        //[Display(Name = "Работа на период")]
        //public string Period { get; set; }
        [Display(Name = "График работы")]
        public string Schedule { get; set; }

        [Display(Name = "Заработная плата (оклад)")]
        public string Salary { get; set; }

        [Display(Name = "Заработная плата (премия)")]
        public string Bonus { get; set; }

        [Display(Name = "Тип договора")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;
        public IList<IdNameDto> Reports { get; set; }
        [Display(Name = "Доп. Предложения/компенсации")]
        public string Compensation { get; set; }

        [Display(Name = "Требования по образованию")]
        public string EducationRequirements { get; set; }

        [Display(Name = "Требования по опыту работы")]
        public string ExperienceRequirements { get; set; }

        [Display(Name = "Прочие требования")]
        public string OtherRequirements { get; set; }

        //[Display(Name = "Доп. требования")]
        //public string AdditionalRequirements { get; set; }

        [Display(Name = "Должностные обязанности")]
        public string Responsibility { get; set; }

        [Display(Name = "Желательная дата выхода")]
        public string DesirableBeginDate { get; set; }

        [Display(Name = "Проверка наличия вакансии")]
        public int IsVacationExists { get; set; }
        public int IsVacationExistsHidden { get; set; }
        public IList<IdNameDto> IsVacationExistsValues;

        //[Display(Name = "Работа с даты")]
        //public string BeginDate { get; set; }

        [Display(Name = "Руководитель (инициатор)")]
        public bool IsManagerApproved { get; set; }
        public bool IsManagerApproveAvailable { get; set; }
        public bool IsManagerRejectAvailable { get; set; }
        public bool IsManagerApprovedHidden { get; set; }
        public string ManagerFio { get; set; }

         [Display(Name = "Вышестоящий руководитель")]
        public bool IsChiefApproved { get; set; }
        public bool IsChiefApproveAvailable { get; set; }
        //public bool IsChiefRejectAvailable { get; set; }
        public bool IsChiefApprovedHidden { get; set; }
        public string ChiefFio { get; set; }

        //[Display(Name = "Кадровая служба")]
        //public bool IsPersonnelApproved { get; set; }
        //public bool IsPersonnelApproveAvailable { get; set; }
        //public bool IsPersonnelApprovedHidden { get; set; }
        //public string PersonnelFio { get; set; }

        [Display(Name = "Служба по подбору")]
        public bool IsStaffApproved { get; set; }
        public bool IsStaffApproveAvailable { get; set; }
        public bool IsStaffReceiveRejectMail { get; set; }
        public bool IsStaffApprovedHidden { get; set; }
        public string StaffFio { get; set; }

        public bool IsEditable { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleteUser { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSaveAvailable { get; set; }
        public bool ReloadPage { get; set; }

        public CommentsModel CommentsModel { get; set; }

        public int StaffCreatorId { get; set; }
        public string StaffCreatorName { get; set; }
        public bool ApproveForAll { get; set; }
        public bool ApproveForAllAvailable { get; set; }
        public IList<IdNameDto> AppointmentTypes { get; set; }
        public int AppointmentType { get; set; }
        public bool BankAccountantAccept { get; set; }
        public int BankAccountantAcceptCount { get; set; }
        public string BankAccountantName { get; set; }
        public IList<CandidatePersonnelDto> Personnels { get; set; }
        public IList<CandidateDto> Candidates { get; set; }
        public IList<IdNameDto> Recruters { get; set; }
        public int Recruter1id { get; set; }
        public int Recruter2id { get; set; }
        public int Recruter3id { get; set; }
        public IList<IdNameDto> AppointmentEducationTypes { get; set; }
        public int AppointmentEducationType { get; set; }
    }
}