using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionHotelsModel
    {
        public int Id { get; set; }

        [Display(Name = "Название гостиницы")]
        public string Name { get; set; }

        [Display(Name = "№ счета")]
        public string Account { get; set; }
        public IList<MissionHotelsDto> Documents { get; set; }
    }
}
