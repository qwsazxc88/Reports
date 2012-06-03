using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class UserListModel
    {
        public string UserName { get; set; }
        [Display(Name = "Фильтр по ролям:")]
        public int RoleId { get; set; }
        public IList<IdNameDto> Roles;
//        public bool IsRolesVisible { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public IList<UserDtoModel> Users { get; set; }
    }

    public class UserDtoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}