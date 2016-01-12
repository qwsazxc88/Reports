using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.Bl
{
    public interface IStaffMovementsBl
    {
        void SaveFact(StaffMovementsFactEditModel model);
        StaffMovementsFactListModel GetFactListModel();
        StaffMovementsFactEditModel GetFactEditModel(int Id);
        GridDefinition GetFactDocuments(StaffMovementsFactListModel model);
        StaffMovementsEditModel GetEditModel(int id);
        void SetModel(StaffMovementsEditModel model);
        void SaveModel(StaffMovementsEditModel model);
        StaffMovementsListModel GetListModel();
        IList<IdNameDto> GetPositionsForDepartment(int id);
        IList<StaffMovementsDto> GetDocuments( int DepartmentId, string UserName, int Number, int Status, int TypeId);
        bool CheckMovementsExist(DateTime date, int UserId,int id);
        void SaveDocsModel(StaffMovementsEditModel model);
        StaffMovementsPrintModel GetPrintModel(int id, int SignerId=0);
    }
}
