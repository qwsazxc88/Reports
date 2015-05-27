using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Dao.Impl;
using NHibernate.Linq;
using Reports.Core.Dto;
namespace Reports.Core.Dao
{
    public interface IMessagesDao:IDao<Messages>
    {
        IList<MessagesDto> GetMessages(int PlaceType,int PlaceId);
    }
}
