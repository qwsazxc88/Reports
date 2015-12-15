using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ManagersModel : AbstractEmploymentModel
    {
        //флажок для сотрудников Экспресс-Волги
        public bool IsVolga { get; set; }

        [Display(Name = "Должность"),
            Required(ErrorMessage = "Обязательное поле")]
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public IEnumerable<SelectListItem> PositionItems { get; set; }

        [Display(Name = "Дата оформления"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }        

        [Display(Name = "Условия приема на работу"),
            StringLength(100, ErrorMessage = "Не более 100 знаков.")]
        public string EmploymentConditions { get; set; } //ok

        [Display(Name = "Вид занятости")]
        public bool IsSecondaryJob { get; set; } //ok

        [Display(Name = "Вид совместительства")]
        public bool IsExternalPTWorker { get; set; } //ok

        [Display(Name = "Испытательный срок"),
            StringLength(3, ErrorMessage = "Не более 3 знаков.")]
        public string ProbationaryPeriod { get; set; } //ok

        [Display(Name = "Должностной оклад согласно штатному расписанию")]
        public decimal? SalaryBasis { get; set; }

        [Display(Name = "Ставка"),
            Required(ErrorMessage = "Обязательное для заполнения поле!")]
        public decimal? SalaryMultiplier { get; set; }

        [Display(Name = "Место работы по ТД (нас. пункт)"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string WorkCity { get; set; } //ok

        [Display(Name = "Фронт/Бэк")]
        public bool IsFront { get; set; } //ok

        [Display(Name = "Размер премии")]
        public decimal? Bonus { get; set; } //ok

        [Display(Name = "Материальная ответственность")]
        public bool IsLiable { get; set; } //ok

        [Display(Name = "Номер заявки в службе подбора персонала"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string RequestNumber { get; set; } //ok
                
        public string RejectingChiefName { get; set; }
        [Display(Name = "Причина отказа")]
        public string ChiefRejectionReason { get; set; }

        [Display(Name = "Согласование (Руководитель)")]
        public bool? ManagerApprovalStatus { get; set; }
        public string ApprovingManagerName { get; set; }
        public DateTime? ManagerApprovalDate { get; set; }
        //[Display(Name = "Список руководителей")]
        public string ManagerApprovalList { get; set; }

        [Display(Name = "Согласование (Вышестоящий руководитель)")]
        public bool? HigherManagerApprovalStatus { get; set; }
        public string ApprovingHigherManagerName { get; set; }
        public DateTime? HigherManagerApprovalDate { get; set; }
        //[Display(Name = "Список вышестоящих руководителей")]
        public string HigherManagerApprovalList { get; set; }

        public IEnumerable<SelectListItem> ApprovalStatuses { get; set; }

        [Display(Name = "Причина отказа")]
        public string ManagerRejectionReason { get; set; }

        [Display(Name = "Причина отказа")]
        public string HigherManagerRejectionReason { get; set; }

        [Display(Name = "ФИО наставника")]
        public string MentorName { get; set; }


        public bool IsApproveByManagerAvailable { get; set; }
        public bool IsApproveByHigherManagerAvailable { get; set; }

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public string MessageStr { get; set; }
        public bool IsDraftM { get; set; } //так как все модели наследуют поле IsDraft, то на вкладках проблематично присвоить значение для параметра необходимой моделипришлось добавить параметр

        [Display(Name = "Текст комментария"),
            StringLength(256, ErrorMessage = "Не более 256 знаков.")]
        public string Comment { get; set; } //ok
        public IList<EmploymentCandidateCommentDto> Comments { get; set; }
        public bool IsAddCommentAvailable { get; set; }
        //public CommentsModel CommentsModel { get; set; }//комментарии
        public DateTime? SendTo1C { get; set; }

        public bool IsCancelApproveAvailale { get; set; }//для показа консультатнам кнопки для отмены согласования
        public bool IsCancelApproveHigherAvailale { get; set; }//для показа консультатнам кнопки для отмены согласования
        public bool IsPyrusDialogVisible { get; set; }

        public bool IsSP { get; set; }//сабмитимся для заполнения списка штатных единиц
        [Display(Name = "Штатная единица")]
        public int UserLinkId { get; set; }
        public IList<IdNameDto> PostUserLinks { get; set; }



        #region Надбавки
        //только для чтения
        [Display(Name = "Районный коэффициент (%)")]
        public decimal? AreaMultiplier { get; set; }

        [Display(Name = "Персональная надбавка")]
        public decimal? PersonalAddition { get; set; } //ok

        [Display(Name = "Должностная надбавка")]
        public decimal? PositionAddition { get; set; } //ok

        [Display(Name = "Территориальная надбавка (руб)")]
        public decimal? AreaAddition { get; set; }

        [Display(Name = "Надбавка за разъездной характер работы (руб)")]
        public decimal? TravelRelatedAddition { get; set; }

        [Display(Name = "Надбавка за квалификацию (руб)")]
        public decimal? CompetenceAddition { get; set; }

        [Display(Name = "Надбавка за стаж работы специалистом фронт-офиса (руб)")]
        public decimal? FrontOfficeExperienceAddition { get; set; }

        [Display(Name = "Итого оплата труда (руб)")]
        public string SalaryTotal { get; set; }

        [Display(Name = "Укажите номер задачи в системе Pyrus")]
        public string PyrusNumber { get; set; }

        public bool IsConsultant { get; set; }  //признак консультанта у текущего пользователя
        #endregion

        public ManagersModel()
        {
            this.Version = 0;
        }
    }
}
