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
        void CheckFillFieldsForGpdRefDetail(GpdRefDetailEditModel model, System.Web.Mvc.ModelStateDictionary ms);
        bool SaveGpdRefDetail(GpdRefDetailEditModel model, out string error);
        void SetGpdRefDetailFind(GpdRefDetailModel model, bool hasError);
        GpdRefDetailEditModel GetMissionOrderEditModel(int id, bool hasError);
        #endregion

        #region Договора
        void SetGpdContractView(GpdContractModel model, bool hasError);
        void SetGpdContractStatuses(GpdContractModel model, bool hasError);
        void SetGpdContractChargingTypes(GpdContractModel model, bool hasError);
        void SetGpdContractChargingTypes(GpdContractEditModel model, bool hasError);
        void SetGpdContractPersons(GpdContractEditModel model, bool hasError);
        GpdContractEditModel SetGpdContractEdit(int Id, bool hasError);
        void SetGpdContractDetails(GpdContractEditModel model, bool hasError);
        void CheckFillFieldsForGpdContract(GpdContractEditModel model, System.Web.Mvc.ModelStateDictionary ms);
        void SetGpdContractStatuses(GpdContractEditModel model, bool hasError);
        bool SaveGpdContract(GpdContractEditModel model, out string error);
        void SetGpdContract(GpdContractModel model);
        #endregion
    }
}
