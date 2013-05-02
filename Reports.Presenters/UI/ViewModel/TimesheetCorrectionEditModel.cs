using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class TimesheetCorrectionEditModel : UserInfoModel, ICheckBoxes
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Вид почасового тарифа  ")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;
        public bool IsTypeEditable { get; set; }
       
        [Display(Name = " Дата (рабочий день)")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "TimesheetCorrectionEditModel_Date_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("TimesheetCorrectionEditModel_Date_Required", typeof(Resources))]
        public DateTime? EventDate { get; set; }

        //[Display(Name = "Кол-во дней компенсации")]
        //public string Compensation { get; set; }
        //public bool IsPersonnelFieldsEditable { get; set; }

        [Required(ErrorMessageResourceName = "TimesheetCorrectionEditModel_Hours_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("TimesheetCorrectionEditModel_Hours_Required", typeof(Resources))]
        [Display(Name = "Часы")]
        public string Hours { get; set; }
        
        [Display(Name = "Заполнение табеля")]
        public int StatusId { get; set; }
        public int StatusIdHidden { get; set; }
        public IList<IdNameDto> Statuses;
        public bool IsStatusEditable { get; set; }

        [Display(Name = "Сотрудник cогласен")]
        public bool IsApprovedByUser { get; set; }
        public bool IsApprovedByUserHidden { get; set; }
        public bool IsApprovedByUserEnable { get; set; }
        //public bool IsApprovedByUserChecked { get; set; }
        [Display(Name = "Руководитель cогласен")]
        public bool IsApprovedByManager { get; set; }
        public bool IsApprovedByManagerHidden { get; set; }
        public bool IsApprovedByManagerEnable { get; set; }
        [Display(Name = "Кадровик cогласен")]
        public bool IsApprovedByPersonnelManager { get; set; }
        public bool IsApprovedByPersonnelManagerHidden { get; set; }
        public bool IsApprovedByPersonnelManagerEnable { get; set; }
        [Display(Name = "Выгружен в 1C")]
        public bool IsPostedTo1C { get; set; }
        public bool IsPostedTo1CHidden { get; set; }
        public bool IsPostedTo1CEnable { get; set; }

        [Display(Name = "Автор")]
        public string CreatorLogin { get; set; }

        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }
        public RequestCommentsModel CommentsModel { get; set; }

        public bool ReloadPage { get; set; }

        public bool IsApproved { get; set; }
        public bool IsApprovedEnable { get; set; }
    }
}
