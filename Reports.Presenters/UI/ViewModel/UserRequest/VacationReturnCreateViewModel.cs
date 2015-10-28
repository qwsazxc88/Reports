using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class VacationReturnCreateViewModel
    {        
        public List<IdNameDto> Users { get; set; }
        [Display(Name = "Сотрудник")]
        public int UserId { get; set; }
        public VacationReturnCreateViewModel()
        {
            this.Users = new List<IdNameDto>();
        }
    }
}
