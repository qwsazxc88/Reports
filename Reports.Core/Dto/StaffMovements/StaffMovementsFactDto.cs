using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class StaffMovementsFactDto
    {
        public StaffMovementsFactDto()
        {
            this.Additions = new List<AdditionsDto>();
            this.UserToMove = new StandartUserDto();
        }
        public int Id { get; set; }
        public DateTime SendTo1C { get; set; }
        public StandartUserDto UserToMove { get; set; }
        public int StaffEstablishedPostRequestId { get; set; }
        public int StaffMovementsId { get; set; }
        public List<AdditionsDto> Additions { get; set; }
    }
}
