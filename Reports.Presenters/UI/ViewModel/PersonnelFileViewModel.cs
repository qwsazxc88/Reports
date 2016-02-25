using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.ViewModel
{
    public class PersonnelFileViewModel
    {
        public IList<IdNameDto> Cities { get; set; }
        public int City { get; set; }
    }
}
