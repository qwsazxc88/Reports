using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IAttachmentDao : IDao<Attachment>
    {
        Attachment FindByDocumentId(int documentId);
    }
}