using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Dao
{
    public interface IRequestAttachmentDao : IDao<RequestAttachment>
    {
        RequestAttachment FindByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type);
        IList<RequestAttachment> FindManyByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type);
        int GetAttachmentsCount(int entityId);
        int DeleteForEntityId(int entityId);
    }
}