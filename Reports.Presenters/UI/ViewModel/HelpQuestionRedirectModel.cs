using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpQuestionRedirectModel
    {
        public IList<IdNameDto> Roles;

        [Display(Name = "Роль")]
        public int RoleId { get; set; }
    }
}