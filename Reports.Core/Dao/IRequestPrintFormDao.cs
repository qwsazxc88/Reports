using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Dao
{
    public interface IRequestPrintFormDao : IDao<RequestPrintForm>
    {
        RequestPrintForm FindByRequestAndTypeId(int requestId, RequestPrintFormTypeEnum typeId);
    }
}