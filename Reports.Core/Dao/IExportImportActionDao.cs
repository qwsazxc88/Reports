using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Dao
{
    public interface IExportImportActionDao : IDao<ExportImportAction>
    {
        IList<ExportImportAction> LoadForTypeSorted(ExportImportType type);
    }
}