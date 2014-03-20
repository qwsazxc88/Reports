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

        [Display(Name = "Должность")]
        public string ReasonPosition { get; set; }

        [Display(Name = "С даты")]
        public string ReasonBeginDate { get; set; }


        public string ReasonPersonnelStore { get; set; }


        public bool IsEditable { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSaveAvailable { get; set; }
        public bool ReloadPage { get; set; }

        public CommentsModel CommentsModel { get; set; }

    }
}