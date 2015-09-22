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
        /// <returns></returns>
        StaffListModel GetDepartmentStructureWithStaffPost(string DepId);

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
        #endregion

        #region Справочник ПО.
        /// <summary>
        /// Загрузка модели справочника ПО.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <returns></returns>
        StaffDepartmentSoftReferenceModel GetSoftReference(StaffDepartmentSoftReferenceModel model);
        /// <summary>
        /// Создание/Сохранение данных.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <param name="error">Для сообщений.</param>
        /// <returns></returns>
        bool SaveSoftReference(StaffDepartmentSoftReferenceModel model, out string error);
        #endregion
        #endregion

        #region Штатная расстановка.
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения и штатную расстановку.
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        StaffListArrangementModel GetDepartmentStructureWithStaffArrangement(string DepId);
        #endregion

        #region Загрузка словарей и справочников.
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
        /// Заполняем список видов заявок для подразделений.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetDepRequestStatuses();
        /// <summary>
        /// Достаем для автозаполнения список должностей.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<IdNameDto> GetPositionAutocomplete(string Name);
        #endregion

        #region Для теста
        TreeViewModel GetDepartmentList();
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        TreeGridAjaxModel GetDepartmentStructure(string DepId);
        IList<StaffListDepartmentDto> GetDepartmentListByParent(string DepId);
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
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения с привязками к точкам Финграда
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        DepStructureFingradPointsModel GetDepartmentStructureWithFingradPoins(string DepId);
        #endregion
    }
}
