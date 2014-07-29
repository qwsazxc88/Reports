using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AppointmentReportEditModel : ManagerInfoModel, IContainId, IAttachment
    {
        public int Version { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion
        [Display(Name = "В структурное подразделение")]
        public string DepartmentName { get; set; }

        [Display(Name = "Место работы (город)")]
        public string City { get; set; }

        [Display(Name = "Должность")]
        public string CandidatePosition { get; set; }

        [Display(Name = "Количество вакансий")]
        public string VacationCount { get; set; }

        [Display(Name = "Основание появления вакансии")]
        public string Reason { get; set; }

        [Display(Name = "ФИО кандидата")]
        public string Name { get; set; }

        [Display(Name = "Телефон кандидата")]
        public string Phone { get; set; }

        [Display(Name = "Email кандидата")]
        public string Email { get; set; }

        [Display(Name = "Дата собеседования")]
        public string ColloquyDate { get; set; }

        [Display(Name = "К заявке №")]
        public string AppointmentNumber { get; set; }

        [Display(Name = "Обучение")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Срок обучения")]
        public string EducationTime { get; set; }

        [Display(Name = "Руководитель (заказчик)")]
        public bool IsManagerApproved { get; set; }
        public bool IsManagerApproveAvailable { get; set; }
        public bool IsManagerRejectAvailable { get; set; }
        public bool IsManagerApprovedHidden { get; set; }
        public string ManagerFio { get; set; }

        [Display(Name = "Служба по подбору")]
        public bool IsStaffApproved { get; set; }
        public bool IsStaffApproveAvailable { get; set; }
        //public bool IsStaffReceiveRejectMail { get; set; }
        public bool IsStaffApprovedHidden { get; set; }
        public string StaffFio { get; set; }
        public bool IsAddAvailable { get; set; }

        public bool IsEditable { get; set; }
        public bool IsManagerEditable { get; set; }
        public bool IsColloquyDateEditable { get; set; }
        public bool IsPrintLoginAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleteUser { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteScanAvailable { get; set; }
        public bool IsStaffSetDateAcceptAvailable { get; set; }
        public bool ReloadPage { get; set; }

        [Display(Name = "Отказано по причине")]
        public string RejectReason { get; set; }

        [Display(Name = "Обучение пройдено/не пройдено")]
        public int IsEducationExists { get; set; }
        public int IsEducationExistsHidden { get; set; }
        public IList<IdNameDto> IsEducationExistsValues;

        [Display(Name = "Собеседование пройдено/не пройдено")]
        public int IsColloquyPassed { get; set; }
        public int IsColloquyPassedHidden { get; set; }
        public IList<IdNameDto> IsColloquyPassedValues;

        [Display(Name = "Кандидат принят на работу с даты")]
        public string DateAccept { get; set; }

        [Display(Name = "Скан")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }

        public CommentsModel CommentsModel { get; set; }
    }
}