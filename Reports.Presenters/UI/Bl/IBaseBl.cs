using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using Reports.Core.Dto;
using System.Collections.Generic;
using System;
using Reports.Core.Domain;
using Reports.Core.Dao;
namespace Reports.Presenters.UI.Bl
{
    public interface IBaseBl
    {
        IUser CurrentUser { get; }
        IList<MessagesDto> GetComments(int PlaceTypeId, int PlaceId);
        void AddComment(MessagesDto message);

        static void ChangeStatus(this StandartRequestEntity entity, IDao<AbstractReferencyBookEntity> RefBookDao, int Status);
        static void ChangeProperties<T, key>(this T entity, Action<T> action) where T : IEntity<key>;

    }
}