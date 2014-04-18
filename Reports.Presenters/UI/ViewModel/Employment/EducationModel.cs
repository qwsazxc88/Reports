using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class EducationModel
    {
        //[Display(Name = "Если изменяли ФИО, укажите их, а также когда меняли, где и по какой причине")]
        public IList<NameChangeDto> NameChanges { get; set; }
    }
}
