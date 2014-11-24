using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionHotelsEditModel
    {
        public int Id { get; set; }
        public bool? flgNew { get; set; }

        [Display(Name = "Название гостиницы")]
        public string Name { get; set; }

        [Display(Name = "№ счета")]
        public string Account { get; set; }

    }
}
