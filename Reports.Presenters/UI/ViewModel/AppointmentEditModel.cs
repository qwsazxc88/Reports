using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

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

        [Display(Name = "Место работы (город)")]
        //[Required(ErrorMessageResourceName = "AppointmentEditModel_City_Required",ErrorMessageResourceType = typeof(Resources))]
        //[LocalizationDisplayName("AppointmentEditModel_City_Required", typeof(Resources))]
        public string City { get; set; }

        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public int PositionIdHidden { get; set; }
        public IList<IdNameDto> Positions;

        [Display(Name = "Количество вакансий")]
        public string VacationCount { get; set; }

        [Display(Name = "Основание появления вакансии")]
        public int ReasonId { get; set; }
        public int ReasonIdHidden { get; set; }
        public IList<IdNameDto> Reasons;

        //[Display(Name = "Должность")]
        public string ReasonPosition { get; set; }
        //[Display(Name = "С даты")]
        public string ReasonBeginDate { get; set; }

        [Display(Name = "Работа на период")]
        public string Period { get; set; }
        [Display(Name = "График работы")]
        public string Schedule { get; set; }

        [Display(Name = "Заработная плата (оклад)")]
        public string Salary { get; set; }

        [Display(Name = "Заработная плата (премия)")]
        public string Bonus { get; set; }

        [Display(Name = "Форма оформления сотрудника")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Доп. Предложения/компенсации")]
        public string Compensation { get; set; }

        [Display(Name = "Требования по образованию")]
        public string EducationRequirements { get; set; }

        [Display(Name = "Требования по опыту работы")]
        public string ExperienceRequirements { get; set; }

        [Display(Name = "Прочие требования")]
        public string OtherRequirements { get; set; }

        [Display(Name = "Доп. требования")]
        public string AdditionalRequirements { get; set; }

        [Display(Name = "Должностные обязанности")]
        public string Responsibility { get; set; }

        [Display(Name = "Желательная дата выхода")]
        public string DesirableBeginDate { get; set; }

        [Display(Name = "Проверка наличия вакансии")]
        public int IsVacationExists { get; set; }
        public int IsVacationExistsHidden { get; set; }
        public IList<IdNameDto> IsVacationExistsValues;

        [Display(Name = "Работа с даты")]
        public string BeginDate { get; set; }


        public bool IsEditable { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSaveAvailable { get; set; }
        public bool ReloadPage { get; set; }

        public CommentsModel CommentsModel { get; set; }

    }
}