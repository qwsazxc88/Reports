using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
namespace Reports.Core.Dao
{
    public interface IDocumentMovementsDao: IDao<DocumentMovements>
    {
    }
    public interface IDocumentMovements_DocTypesDao : IDao<DocumentMovements_DocTypes>
    {
    }
    public interface IDocumentMovements_SelectedDocsDao : IDao<DocumentMovements_SelectedDocs>
    {
    }
    public interface IDocumentMovementsRoleRecordsDao : IDao<DocumentMovementsRoleRecords>
    {
    }
}
