using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class MessagesDto
    {
        public string CreatorName { get; set; }
        public string ReceiverName { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorId { get; set; }
        public int ReceiverId { get; set; }
        public int PlaceId { get; set; }
        public int PlaceTypeId { get; set; }

    }
}
