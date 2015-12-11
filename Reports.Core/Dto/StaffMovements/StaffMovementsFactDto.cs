using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class StaffMovementsFactDto
    {
        public int Id { get; set; }
        public DateTime SendTo1C { get; set; }
        public UserDto User { get; set; }
        public int StaffEstablishedPostRequestId { get; set; }
        public int StaffMovementsId { get; set; }
        public List<AdditionsDto> Additions { get; set; }

    }
}
