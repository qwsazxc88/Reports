using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;

namespace Reports.Core.Dao
{
    public interface IRequestAttachmentDao : IDao<RequestAttachment>
    {
        RequestAttachment FindByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type);
        IList<RequestAttachment> FindManyByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type);
        //int GetAttachmentsCount(int entityId);
        int DeleteForEntityId(int entityId,RequestAttachmentTypeEnum type);
        IList<IdContextDto> FindOld(int beforeId);
        int UpdateAttachement(int id, byte[] context);
        List<RequestAttachment> FindOldEntities(int beforeId);
        void Evict(RequestAttachment entity);
    }
}