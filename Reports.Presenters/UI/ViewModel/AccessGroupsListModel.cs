using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using System.Web.Mvc;

namespace Reports.Presenters.UI.ViewModel
{
    public class AccessGroupsListModel
    {
        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "Группа доступа")]
        public string AccessGroupCode { get; set; }
        public IEnumerable<SelectListItem> AccessGroups { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Показать руководителей")]
        public bool IsManagerShow { get; set; }

        [Display(Name = "ФИО руководителя 6 уровня")]
        public string Manager6 { get; set; }

        [Display(Name = "ФИО руководителя 5 уровня")]
        public string Manager5 { get; set; }

        [Display(Name = "ФИО руководителя 4 уровня")]
        public string Manager4 { get; set; }

        public IList<AccessGroupListDto> AccessGroupList { get; set; }
        public int SortBy { get; set; }
        public bool SortDescending { get; set; }
        public bool IsPhoneEditable { get; set; }
        public bool IsAlternativeMailEditable { get; set; }
        
    }
}
