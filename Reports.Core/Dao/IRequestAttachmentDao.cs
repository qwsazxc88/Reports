using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Dao
{
    public interface IRequestAttachmentDao : IDao<RequestAttachment>
    {
        RequestAttachment FindByRequestIdAndTypeId(int id, RequestTypeEnum type);
    }
}