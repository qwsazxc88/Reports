using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Dao.Impl;
using NHibernate.Linq;
using Reports.Core.Dto;
using Reports.Core.Services;
namespace Reports.Core.Dao
{
    public class MessagesDao:DefaultDao<Messages>, IMessagesDao
    {
        public MessagesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<MessagesDto> GetMessages(int PlaceType,int PlaceId)
        {
            var query=Session.Query<Messages>();
            var result=query.Where(x=>x.CommentPlaceType==PlaceType && x.PlaceId==PlaceId)
                .Select(y=>new MessagesDto{ Comment=y.Comment, CreatorName=y.Creator.Name, CreateDate=y.CreateDate, CreatorId=y.Creator.Id, ReceiverId=y.Receiver!=null?y.Receiver.Id:0, ReceiverName=y.Receiver!=null?y.Receiver.Name:""});
            return result.ToList();
        }

    }
}
