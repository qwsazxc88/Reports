using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AppointmentSelectManagerModel
    {
        [Display(Name = "Руководитель")]
        public int ManagerId { get; set; }

        public List<IdNameDto> Managers { get; set; }
    }
}