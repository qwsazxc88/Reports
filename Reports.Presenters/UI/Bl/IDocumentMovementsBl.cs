using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using Reports.Core;
using Reports.Presenters.UI.ViewModel;
namespace Reports.Presenters.UI.Bl
{
    public interface IDocumentMovementsBl: IBaseBl
    {
        DocumentMovementsListModel GetListModel();
        GridDefinition GetDocuments(DocumentMovementsListModel model);
        DocumentMovementsEditModel GetEditModel(int Id);
        DocumentMovementsEditModel SaveModel(DocumentMovementsEditModel model);
        List<IdNameDto> GetRuscountUsers(string query);
    }
}
