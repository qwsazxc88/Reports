using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class DateFieldViewModel: FieldViewModel<DateTime?>
    {       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd.MM.yyyy")]
        public override DateTime? Value { get; set; }
    }
}
