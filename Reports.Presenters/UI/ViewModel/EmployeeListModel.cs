using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EmployeeListModel
    {
        public string UserName { get; set; }
        public bool IsUserNameVisible { get; set; }
        [Display(Name = "Фильтр по ролям:")]
        public int RoleId { get; set; }
        public IList<IdNameDto> Roles;
        public bool IsRolesVisible { get; set; }
        public IList<EmployeeDtoModel> Employees { get; set; }
        public bool IsLinkToUser { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
    public class EmployeeDtoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
