using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class Messages:AbstractEntity
    {
        public virtual User Creator {get;set;}
        public virtual User Receiver { get; set; }
        public virtual int CommentPlaceType { get; set; }
        public virtual int PlaceId { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
