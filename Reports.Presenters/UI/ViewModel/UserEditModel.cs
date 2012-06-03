using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class UserEditModel : IEmailDtoSupport
    {
        public int Id { get; set; }
        public int Version { get; set; }
        [Display(Name = "Пользователь")]
        public string UserNameStatic { get; set; }
        [Display(Name = "ФИО")]
        [Required(ErrorMessageResourceName = "UserEditModel_UserName_Required",
        ErrorMessageResourceType = typeof(Resources))]
        public string UserName { get; set; }
        public bool IsUserNameEditable { get; set; }
        [Display(Name = "Логин")]
        [Required(ErrorMessageResourceName = "UserEditModel_Login_Required",
        ErrorMessageResourceType = typeof(Resources))]
        public string Login { get; set; }
        public bool IsLoginEditable { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessageResourceName = "UserEditModel_Password_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [ValidatePasswordLength]
        public string Password { get; set; }
        [Email]
        [Display(Name = "Адрес эл. почты")]
        public string Email { get; set; }
        [Display(Name = "Код 1C")]
        public string Code { get; set; }
        [Display(Name = "Роль")]
        public int RoleId { get; set; }
        public IList<IdNameDto> Roles;
        public bool IsRoleEditable { get; set; }
        [Display(Name = "Руководитель")]
        public int ManagerId { get; set; }
        public IList<IdNameDto> Managers;
        public bool IsManagerEditable { get; set; }
        [Display(Name = "Кадровик")]
        public int PersonnelId { get; set; }
        public IList<IdNameDto> Personnels;
        public bool IsPersonnelEditable { get; set; }
        [Display(Name = "Активный")]
        public bool IsActive { get; set; }
        public bool IsActiveEditable { get; set; }
        public bool IsActiveHidden { get; set; }

        public string Error { get; set; }
        public bool NeedToReload { get; set; }
        public bool ClearManagers { get; set; }

        public EmailDto EmailDto { get; set; }
    }
}