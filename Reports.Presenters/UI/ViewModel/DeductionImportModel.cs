using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using System.Web;
namespace Reports.Presenters.UI.ViewModel
{
    public class DeductionImportModel
    {
        [Display(Name = "Дата создания служебной записки")]
        public string DateEdited { get; set; }

        [Display(Name = "Вид удержания")]
        public int KindId { get; set; }
        public int KindIdHidden { get; set; }
        public IList<IdNameDto> Kindes;

        [Display(Name = "Период")]
        public int MonthId { get; set; }
        public int MonthIdHidden { get; set; }
        public IList<IdNameDto> Monthes;

        [Display(Name = "Тип заявки")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Автор")]
        public string Editor { get; set; }

        public HttpPostedFileBase File { get; set; }
        public IList<string> Errors { get; set; }
        public IList<DeductionDto> Imported { get; set; }
    }
}
