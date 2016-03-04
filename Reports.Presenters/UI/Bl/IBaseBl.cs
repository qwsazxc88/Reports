using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using Reports.Core.Dto;
using System.Collections.Generic;
using System;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core;
namespace Reports.Presenters.UI.Bl
{
    public interface IBaseBl
    {
        IUser CurrentUser { get; }
        IList<MessagesDto> GetComments(int PlaceTypeId, int PlaceId);
        void AddComment(MessagesDto message);
        string AddAlternativeMail(string Email);
        string AddAlternativeMail(int UserId, string Email);
        void AddPhone(int UserId, string Phone);
        Result ConfirmMail(Guid key);
        DepartmentTreeDto GetDepartmentTree();
    }

}