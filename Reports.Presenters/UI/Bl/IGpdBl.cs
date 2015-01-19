using System;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.Bl
{
    public interface IGpdBl : IBaseBl
    {
        #region Справочник реквизитов
        void SetGpdRefDetailView(GpdRefDetailModel model, bool hasError);
        void SetGpdRefDetailTypes(GpdRefDetailEditModel model);
        void SetGpdRefDetailTypes(GpdRefDetailModel model);
        void CheckFillFieldsForGpdRefDetail(GpdRefDetailEditModel model, System.Web.Mvc.ModelStateDictionary ms, bool flgFromContract);
        bool SaveGpdRefDetail(GpdRefDetailEditModel model, out string error);
        void SetGpdRefDetailFind(GpdRefDetailModel model, bool hasError);
        GpdRefDetailEditModel SetRefDetailEditModel(int Id, int StatusID, int Operation, bool flgView, int DTID, int PayerID, int PayeeID, int DetailId, int PersonID);
        void GetPermission(GpdRefDetailModel model);
        void GetPermission(GpdRefDetailEditModel model);
        IList<GpdContractSurnameDto> GetPersonAutocomplete(string Name, int PersonID);
        #endregion

        #region Договоры
        void SetGpdContractView(GpdContractModel model);
        void SetGpdContractStatuses(GpdContractModel model);
        void SetGpdContractChargingTypes(GpdContractModel model);
        void SetGpdContractChargingTypes(GpdContractEditModel model);
        void SetGpdContractPersons(GpdContractEditModel model);
        GpdContractEditModel SetGpdContractEdit(int Id, int PersonID, int DepId, string DepName);
        GpdContractEditModel SetGpdContractEdit(GpdContractEditModel model);
        void CheckFillFieldsForGpdContract(GpdContractEditModel model, System.Web.Mvc.ModelStateDictionary ms);
        void SetGpdContractStatuses(GpdContractEditModel model);
        bool SaveGpdContract(GpdContractEditModel model, out string error);
        void SetGpdContract(GpdContractModel model);
        void GetPermission(GpdContractModel model);
        void GetPermission(GpdContractEditModel model);
        void SetGpdContractEditDropDowns(GpdContractEditModel model);
        IList<GpdContractSurnameDto> GetPersonDSAutocomplete(string Name, int PersonID);
        #endregion

        #region Акты
        GpdActEditModel SetActEditModel(int Id, int GCID, bool hasError);
        void CheckFillFieldsForGpdAct(GpdActEditModel model, System.Web.Mvc.ModelStateDictionary ms);
        bool SaveGpdAct(GpdActEditModel model, out string error);
        void SetGpdActFind(GpdActListModel model, bool hasError);
        void SetGpdActView(GpdActListModel model, bool hasError);
        void GetPermission(GpdActListModel model);
        void GetPermission(GpdActEditModel model);
        #endregion

        #region Модальное окно создания/редактирования реквизитов
        GpdRefDetailDialogModel SetDetailDialog(int ID);
        #endregion
    }
}
