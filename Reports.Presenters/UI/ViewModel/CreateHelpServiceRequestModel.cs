using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class CreateHelpServiceRequestModel
    {
        public IList<IdNameDto> Users;

        public CreateHelpServiceRequestModel()
        {
            Users = new List<IdNameDto>();
        }

        [Display(Name = "За сотрудника")]
        public int UserId { get; set; }
    }
}