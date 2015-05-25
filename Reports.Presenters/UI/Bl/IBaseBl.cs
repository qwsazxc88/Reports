using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using Reports.Core.Dto;
using System.Collections.Generic;
namespace Reports.Presenters.UI.Bl
{
    public interface IBaseBl
    {
        IUser CurrentUser { get; }
        IList<MessagesDto> GetComments(int PlaceTypeId, int PlaceId);
        void AddComment(MessagesDto message);
    }
}