using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Reports.Core.Dto
{
    public class UserPersonnelDataDto
    {
        public int UserId { get; set; }
        public int PeopleId  { get; set; }
        public string UserName { get; set; }
        public string FIO { get; set; }
        public string Position { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public string Snils { get; set; }
        public string INN { get; set; }
        [DisplayFormat(DataFormatString="{0:dd.MM.yyyy}")]
        public DateTime? DateAccept { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? DateRelease { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? chilvacationend { get; set; }
        public bool GPDFlag { get; set; }
        public bool UserFlag { get; set; }
        public int DepId { get; set; }
    }
}
