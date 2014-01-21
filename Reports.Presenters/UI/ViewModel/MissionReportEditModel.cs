﻿using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionReportEditModel : UserInfoModel, IContainId
    {
        public int Version { get; set; }
        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion

        public string Costs { get; set; }

        //[Display(Name = "")]
        public string UserFio { get; set; }
        public bool IsUserApproved { get; set; }
        public bool IsUserApprovedAvailable { get; set; }
        public bool IsUserApprovedHidden { get; set; }

        [Display(Name = "Задание сотрудника выполнено, производственные расходы согласованы")]
        public bool IsManagerApproved { get; set; }
        public bool IsManagerApprovedAvailable { get; set; }
        public bool IsManagerApprovedHidden { get; set; }

        [Display(Name = "Авансовый отчет проверен")]
        public bool IsAccountantApproved { get; set; }
        public bool IsAccountantApproveAvailable { get; set; }
        public bool IsAccountantApprovedHidden { get; set; }

        [Display(Name = "Бухгалтер")]
        public string AccountantFio { get; set; }
        //public string AccountantEmail { get; set; }

        public bool IsEditable { get; set; }
        public bool IsSaveAvailable { get; set; }

        public bool IsAccountantEditable { get; set; }

        public bool ReloadPage { get; set; }

        public RequestCommentsModel CommentsModel { get; set; }
    }
}