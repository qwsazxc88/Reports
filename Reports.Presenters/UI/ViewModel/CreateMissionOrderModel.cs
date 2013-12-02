using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class CreateMissionOrderModel
    {
        public IList<IdNameDto> Users;

        public CreateMissionOrderModel()
        {
            Users = new List<IdNameDto>();
        }

        [Display(Name = "За пользователя")]
        public int UserId { get; set; }
    }
}