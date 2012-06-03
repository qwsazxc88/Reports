using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class UserModel
    {
        public int? UserId { get; set; }
        public string FullName { get; set; }
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Display(Name = "Дата приема")]
        [DataType(DataType.Date)]
        public DateTime? DateAccepted { get; set; }
        [Display(Name = "Дата увольнения")]
        [DataType(DataType.Date)]
        public DateTime? DateRelease { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
    }
}