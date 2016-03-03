using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.Bl
{
    public interface IStaffListBl : IBaseBl
    {
        #region Штатные расписание.
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения и штатные единицы
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <param name="IsBegin">Флажок показывающий, что это первоначальная загрузка.</param>
        /// <returns></returns>
        StaffListModel GetDepartmentStructureWithStaffPost(string DepId, bool IsBegin);

        #region Заявки для подразделений
        /// <summary>
        /// Загрузка запросной формы реестра заявок подразделений.
        /// </summary>
        /// <returns></returns>
        StaffDepartmentRequestListModel GetStaffDepartmentRequestList();
        /// <summary>
        /// Загрузка реестра заявок подразделений.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        StaffDepartmentRequestListModel SetStaffDepartmentRequestList(StaffDepartmentRequestListModel model);
        /// <summary>
        /// Заполняем модель заявки на создание подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <returns></returns>
        StaffDepartmentRequestModel GetDepartmentRequest(StaffDepartmentRequestModel model);
        /// <summary>
        /// Процедура сохранения новой заявки для подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        bool SaveNewDepartmentRequest(StaffDepartmentRequestModel model, out string error);
        /// <summary>
        /// Процедура сохранения существующей заявки для подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        bool SaveEditDepartmentRequest(StaffDepartmentRequestModel model, out string error);
        /// <summary>
        /// Сохраняем админами по банка коды.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="error">Сообщение</param>
        /// <returns></returns>
        bool SaveProgramCodes(StaffDepartmentRequestModel model, out string error);
        /// <summary>
        /// Процедура отклонения существующей заявки для подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        bool DeleteDepartmentRequest(StaffDepartmentRequestModel model, out string error);
        #endregion

        #region Заявки для штатных единиц
        /// <summary>
        /// Загрузка запросной формы реестра заявок ШЕ.
        /// </summary>
        /// <returns></returns>
        StaffEstablishedPostRequestListModel GetStaffEstablishedPostRequestList();
        /// <summary>
        /// Загрузка реестра заявок ШЕ.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        StaffEstablishedPostRequestListModel SetStaffEstablishedPostRequestList(StaffEstablishedPostRequestListModel model);
        /// <summary>
        /// Заполняем модель заявки для штатной единицы.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <returns></returns>
        StaffEstablishedPostRequestModel GetEstablishedPostRequest(StaffEstablishedPostRequestModel model);
        /// <summary>
        /// Процедура сохранения новой заявки для штатной единицы.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        bool SaveNewEstablishedPostRequest(StaffEstablishedPostRequestModel model, out string error);
        /// <summary>
        /// Процедура сохранения существующей заявки для штатной единицы.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        bool SaveEditEstablishedPostRequest(StaffEstablishedPostRequestModel model, out string error);
        /// <summary>
        /// Процедура отклонения существующей заявки для штатной единицы.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        bool DeleteEstablishedPostRequest(StaffEstablishedPostRequestModel model, out string error);
        #endregion

        #region Заявки на создание вакансий при длительном отсутствии сотрудников.
        /// <summary>
        /// Загрузка формы реестра для заявок на создание временных вакансий.
        /// </summary>
        /// <returns></returns>
        StaffTemporaryReleaseVacancyRequestListModel GetStaffTemporaryReleaseVacancyRequestList();
        /// <summary>
        /// Загрузка реестра для заявок на создание временных вакансий.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        StaffTemporaryReleaseVacancyRequestListModel SetStaffTemporaryReleaseVacancyRequestListModel(StaffTemporaryReleaseVacancyRequestListModel model);
        /// <summary>
        /// Заполняем модель заявки.
        /// </summary>
        /// <param name="Id">Id заявки.</param>
        /// <returns></returns>
        StaffTemporaryReleaseVacancyRequestModel GetStaffTemporaryReleaseVacancyRequest(int Id);
        /// <summary>
        /// сохраняем изменения заявки заявки.
        /// </summary>
        /// <param name="Id">Id заявки.</param>
        /// <returns></returns>
        bool SaveStaffTemporaryReleaseVacancyRequest(StaffTemporaryReleaseVacancyRequestModel model, out string error);
        /// <summary>
        /// Создание заявки на вакансию при длительном отсутствии сотрудника. 
        /// </summary>
        /// <param name="model">обрабатываемая модель.</param>
        /// <param name="error">Сообщение</param>
        /// <returns></returns>
        bool CreateTemporaryReleaseVacancyRequest(StaffListArrangementModel model, out string error);
        #endregion

        #region Справочник ПО.
        #region Справочник групп ПО
        /// <summary>
        /// Загрузка справочника групп ПО.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        StaffDepartmentSoftGroupModel GetStaffDepartmentSoftGroup(StaffDepartmentSoftGroupModel model);

        /// <summary>
        /// Сохраняем данные справочника групп ПО.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentSoftGroup(StaffDepartmentSoftGroupDto itemToAddEdit, out string error);

        /// <summary>
        /// Проверка сохраняемой строки справочника групп ПО.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentSoftGroupRow(StaffDepartmentSoftGroupDto Row, out string error);
        #endregion

        #region Справочник банковского ПО
        /// <summary>
        /// Загрузка справочника банковского ПО.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        StaffDepartmentInstallSoftModel GetStaffDepartmentInstallSoft(StaffDepartmentInstallSoftModel model);

        /// <summary>
        /// Сохраняем данные справочника банковского ПО.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentInstallSoft(StaffDepartmentInstallSoftDto itemToAddEdit, out string error);

        /// <summary>
        /// Проверка сохраняемой строки справочника банковского ПО.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentInstallSoftRow(StaffDepartmentInstallSoftDto Row, out string error);
        #endregion

        #region Связи групп с ПО
        /// <summary>
        /// Загрузка связей.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="SoftGroupId">Id группы ПО</param>
        /// <returns></returns>
        StaffDepartmentSoftGroupLinksModel GetStaffDepartmentSoftGroupLinks(StaffDepartmentSoftGroupLinksModel model, int SoftGroupId);

        /// <summary>
        /// Сохраняем данные связей ПО с группами.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="SoftGroupId">Id группы ПО</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentSoftGroupLinks(IList<StaffDepartmentSoftGroupLinksDto> itemToAddEdit, int SoftGroupId, out string error);
        #endregion
        #endregion

        #region Справочник кодировок
        #region Справочник филиалов
        /// <summary>
        /// Загрузка справочник кодировок филиалов.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        StaffDepartmentBranchModel GetStaffDepartmentBranch(StaffDepartmentBranchModel model);
        /// <summary>
        /// Сохраняем данные справочника кодировок филиалов.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentBranch(StaffDepartmentBranchDto itemToAddEdit, out string error);
        /// <summary>
        /// Удаляе строки в справочнике кодировок филиалов.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteStaffDepartmentBranch(int Id, out string error);
        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок филиалов.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentBranchRow(StaffDepartmentBranchDto Row, out string error);
        #endregion

        #region Справочник дирекций
        /// <summary>
        /// Загрузка справочника кодировок дирекций.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        StaffDepartmentManagementModel GetStaffDepartmentManagement(StaffDepartmentManagementModel model);
        /// <summary>
        /// Сохраняем данные справочника кодировок дирекций.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentManagement(StaffDepartmentManagementDto itemToAddEdit, out string error);
        /// <summary>
        /// Удаляе строки в справочнике кодировок дирекций.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteStaffDepartmentManagement(int Id, out string error);
        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок дирекций.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentManagementRow(StaffDepartmentManagementDto Row, out string error);
        #endregion

        #region Справочник управлений
        /// <summary>
        /// Загрузка справочника кодировок управлений.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        StaffDepartmentAdministrationModel GetStaffDepartmentAdministration(StaffDepartmentAdministrationModel model, int ManagementFilterId, int BranchFilterId);
        /// <summary>
        /// Сохраняем данные справочника кодировок управлений.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentAdministration(StaffDepartmentAdministrationDto itemToAddEdit, out string error);
        /// <summary>
        /// Удаляе строки в справочнике кодировок управлений.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteStaffDepartmentAdministration(int Id, out string error);
        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок управлений.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentAdministrationRow(StaffDepartmentAdministrationDto Row, out string error);
        #endregion

        #region Справочник бизнес-групп
        /// <summary>
        /// Загрузка справочника кодировок бизнес-групп.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        StaffDepartmentBusinessGroupModel GetStaffDepartmentBusinessGroup(StaffDepartmentBusinessGroupModel model, int AdminFilterId, int ManagementFilterId, int BranchFilterId);
        /// <summary>
        /// Сохраняем данные справочника кодировок бизнес-групп.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentBusinessGroup(StaffDepartmentBusinessGroupDto itemToAddEdit, out string error);
        /// <summary>
        /// Удаляе строки в справочнике кодировок бизнес-групп.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteStaffDepartmentBusinessGroup(int Id, out string error);
        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок бизнес-групп.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentBusinessGroupRow(StaffDepartmentBusinessGroupDto Row, out string error);
        #endregion

        #region Справочник РП-привязок
        /// <summary>
        /// Загрузка справочника кодировок РП-привязок.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="BGFilterId">Id бизнес-группы</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        StaffDepartmentRPLinkModel GetStaffDepartmentRPLink(StaffDepartmentRPLinkModel model, int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId);
        /// <summary>
        /// Сохраняем данные справочника кодировок РП-привязок.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentRPLink(StaffDepartmentRPLinkDto itemToAddEdit, out string error);
        /// <summary>
        /// Удаляе строки в справочнике кодировок РП-привязок.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteStaffDepartmentRPLink(int Id, out string error);
        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок РП-привязок.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentRPLinkRow(StaffDepartmentRPLinkDto Row, out string error);
        #endregion
        #endregion

        #region Справочник операций подразделений

        #region Справочник групп операций
        /// <summary>
        /// Загрузка справочника групп операций.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        StaffDepartmentOperationGroupsModel GetStaffDepartmentOperationGroups(StaffDepartmentOperationGroupsModel model);
        /// <summary>
        /// Сохраняем данные справочника групп операций.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentOperationGroups(StaffDepartmentOperationGroupsDto itemToAddEdit, out string error);
        /// <summary>
        /// Проверка сохраняемой строки справочника групп операций.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentOperationGroupsRow(StaffDepartmentOperationGroupsDto Row, out string error);
        #endregion

        #region Справочник операций
        /// <summary>
        /// Загрузка справочника операций.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        StaffDepartmentOperationsModel GetStaffDepartmentOperations(StaffDepartmentOperationsModel model);

        /// <summary>
        /// Сохраняем данные справочника операций.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentOperations(StaffDepartmentOperationsDto itemToAddEdit, out string error);

        /// <summary>
        /// Проверка сохраняемой строки справочника операций.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ValidateDepartmentOperationRow(StaffDepartmentOperationsDto Row, out string error);
        #endregion

        #region Связи групп с операциями
        /// <summary>
        /// Загрузка связей.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="OperationGroupId">Id группы операций</param>
        /// <returns></returns>
        StaffDepartmentOperationLinksModel GetStaffDepartmentOperationLinks(StaffDepartmentOperationLinksModel model, int OperationGroupId);

        /// <summary>
        /// Сохраняем данные связей операций с группами.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="OperationGroupId">Id группы операций</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool SaveStaffDepartmentOperationLinks(IList<StaffDepartmentOperationLinksDto> itemToAddEdit, int OperationGroupId, out string error);
        #endregion

        #endregion
        #endregion

        #region Штатная расстановка.
        /// <summary>
        /// Загрузка модели страницы (часть с заявкой для временной вакансии)
        /// </summary>
        /// <param name="model">Заполняемая модель.</param>
        /// <returns></returns>
        StaffListArrangementModel GetStaffListArrangementModel(StaffListArrangementModel model);
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения и штатную расстановку.
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <param name="IsBegin">Флажок показывающий, что это первоначальная загрузка.</param>
        /// <returns></returns>
        StaffListArrangementModel GetDepartmentStructureWithStaffArrangement(string DepId, bool IsBegin);
        /// <summary>
        /// Вызов формы для выбора типа заявки в штатном перемещении.
        /// </summary>
        /// <param name="UserId">Id сотрудника.</param>
        /// <returns></returns>
        SelectMovementTypeModel GetSelectMovementTypeModel(int UserId);
        #endregion

        #region Загрузка словарей и справочников.
        /// <summary>
        /// подгружаем только подчиненые ветки на один уровень ниже
        /// </summary>
        /// <param name="DepId">Id родительского подразделения</param>
        /// <param name="IsParentDepOnly">Признак достать только родительское подазделение.</param>
        /// <returns></returns>
        IList<StaffListDepartmentDto> GetDepartmentListByParent(string DepId, bool IsParentDepOnly, bool IsBegin);
        /// <summary>
        /// Загрузка справочников модели для заявок к подразделениям.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        void LoadDictionaries(StaffDepartmentRequestModel model);
        /// <summary>
        /// Загрузка справочников модели для заявок к штатным единицам.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        void LoadDictionaries(StaffEstablishedPostRequestModel model);
        /// <summary>
        /// Загрузка справочников модели для заявок на создание временных вакансий при длительном отсутствии сотрудников.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        void LoadDictionaries(StaffTemporaryReleaseVacancyRequestModel model);
        /// <summary>
        /// Заполняем список видов заявок для подразделений.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetRequestStatuses();
        /// <summary>
        /// Достаем для автозаполнения список должностей.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<IdNameDto> GetPositionAutocomplete(string Name);
        /// <summary>
        /// Загружаем модель для составления Российских адресов.
        /// </summary>
        /// <param name="model">Модель формы</param>
        /// <returns></returns>
        AddressModel GetAddress(AddressModel model);
        /// <summary>
        /// Загружаем список объектов частей адресов.
        /// </summary>
        /// <param name="Code">Код объекта.</param>
        /// <param name="AddressType">Тип объекта.</param>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <param name="CityCode">Код города.</param>
        /// <param name="SettlementCode">Код населенного пункта.</param>
        /// <returns></returns>
        KladrWithPostIndex GetKladr(string Code, int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode);
        #endregion
    }
}
