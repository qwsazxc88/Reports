using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.Bl;
using System.Web.Mvc;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
namespace Reports.Presenters.UI.Bl.Impl
{
    
    public class ShortCodeBl : BaseBl,IShortCodeBl
    {
        #region Dependencies
        protected IAppointmentReasonDao appointmentreasonDao;
        public IAppointmentReasonDao AppointmentReasonDao
        {
            get { return Validate.Dependency(appointmentreasonDao); }
            set { appointmentreasonDao = value; }
        }
        protected IUserLoginDao userLoginDao;
        public IUserLoginDao UserLoginDao
        {
            get { return Validate.Dependency(userLoginDao); }
            set { userLoginDao = value; }
        }
        protected IKladrDao kladrDao;
        public IKladrDao KladrDao
        {
            get { return Validate.Dependency(kladrDao); }
            set { kladrDao = value; }
        }

        protected IStaffDepartmentRequestDao staffdepartmentrequestDao;
        public IStaffDepartmentRequestDao StaffDepartmentRequestDao
        {
            get { return Validate.Dependency(staffdepartmentrequestDao); }
            set { staffdepartmentrequestDao = value; }
        }

        protected IStaffDepartmentCBDetailsDao staffdepartmentCBdetailsDao;
        public IStaffDepartmentCBDetailsDao StaffDepartmentCBDetailsDao
        {
            get { return Validate.Dependency(staffdepartmentCBdetailsDao); }
            set { staffdepartmentCBdetailsDao = value; }
        }

        protected IStaffDepartmentTypesDao staffdepartmenttypesDao;
        public IStaffDepartmentTypesDao StaffDepartmentTypesDao
        {
            get { return Validate.Dependency(staffdepartmenttypesDao); }
            set { staffdepartmenttypesDao = value; }
        }

        protected IStaffProgramCodesDao staffprogramcodesDao;
        public IStaffProgramCodesDao StaffProgramCodesDao
        {
            get { return Validate.Dependency(staffprogramcodesDao); }
            set { staffprogramcodesDao = value; }
        }

        protected IStaffDepartmentLandmarksDao staffdepartmentlandmarksDao;
        public IStaffDepartmentLandmarksDao StaffDepartmentLandmarksDao
        {
            get { return Validate.Dependency(staffdepartmentlandmarksDao); }
            set { staffdepartmentlandmarksDao = value; }
        }

        protected IStaffDepartmentOperationLinksDao staffdepartmentoperationlinksDao;
        public IStaffDepartmentOperationLinksDao StaffDepartmentOperationLinksDao
        {
            get { return Validate.Dependency(staffdepartmentoperationlinksDao); }
            set { staffdepartmentoperationlinksDao = value; }
        }

        protected IRefAddressesDao refaddressesDao;
        public IRefAddressesDao RefAddressesDao
        {
            get { return Validate.Dependency(refaddressesDao); }
            set { refaddressesDao = value; }
        }

        protected IStaffDepartmentOperationsDao staffdepartmentoperationsDao;
        public IStaffDepartmentOperationsDao StaffDepartmentOperationsDao
        {
            get { return Validate.Dependency(staffdepartmentoperationsDao); }
            set { staffdepartmentoperationsDao = value; }
        }

        protected IStaffProgramReferenceDao staffprogramreferenceDao;
        public IStaffProgramReferenceDao StaffProgramReferenceDao
        {
            get { return Validate.Dependency(staffprogramreferenceDao); }
            set { staffprogramreferenceDao = value; }
        }

        protected IStaffLandmarkTypesDao stafflandmarktypesDao;
        public IStaffLandmarkTypesDao StaffLandmarkTypesDao
        {
            get { return Validate.Dependency(stafflandmarktypesDao); }
            set { stafflandmarktypesDao = value; }
        }

        protected IStaffDepartmentRequestTypesDao staffdepartmentrequestTypesDao;
        public IStaffDepartmentRequestTypesDao StaffDepartmentRequestTypesDao
        {
            get { return Validate.Dependency(staffdepartmentrequestTypesDao); }
            set { staffdepartmentrequestTypesDao = value; }
        }

        protected IStaffDepartmentTaxDetailsDao staffdepartmenttaxDetailsDao;
        public IStaffDepartmentTaxDetailsDao StaffDepartmentTaxDetailsDao
        {
            get { return Validate.Dependency(staffdepartmenttaxDetailsDao); }
            set { staffdepartmenttaxDetailsDao = value; }
        }

        protected IStaffDepartmentOperationModesDao staffdepartmentOperationModesDao;
        public IStaffDepartmentOperationModesDao StaffDepartmentOperationModesDao
        {
            get { return Validate.Dependency(staffdepartmentOperationModesDao); }
            set { staffdepartmentOperationModesDao = value; }
        }

        protected IStaffEstablishedPostDao staffestablishedPostDao;
        public IStaffEstablishedPostDao StaffEstablishedPostDao
        {
            get { return Validate.Dependency(staffestablishedPostDao); }
            set { staffestablishedPostDao = value; }
        }

        protected IStaffEstablishedPostRequestTypesDao staffestablishedPostRequestTypesDao;
        public IStaffEstablishedPostRequestTypesDao StaffEstablishedPostRequestTypesDao
        {
            get { return Validate.Dependency(staffestablishedPostRequestTypesDao); }
            set { staffestablishedPostRequestTypesDao = value; }
        }

        protected IStaffEstablishedPostRequestDao staffestablishedPostRequestDao;
        public IStaffEstablishedPostRequestDao StaffEstablishedPostRequestDao
        {
            get { return Validate.Dependency(staffestablishedPostRequestDao); }
            set { staffestablishedPostRequestDao = value; }
        }

        protected IStaffEstablishedPostChargeLinksDao staffestablishedPostChargeLinksDao;
        public IStaffEstablishedPostChargeLinksDao StaffEstablishedPostChargeLinksDao
        {
            get { return Validate.Dependency(staffestablishedPostChargeLinksDao); }
            set { staffestablishedPostChargeLinksDao = value; }
        }

        protected IStaffExtraChargesDao staffextraChargesDao;
        public IStaffExtraChargesDao StaffExtraChargesDao
        {
            get { return Validate.Dependency(staffextraChargesDao); }
            set { staffextraChargesDao = value; }
        }

        protected IStaffDepartmentReasonsDao staffsepartmentReasonsDao;
        public IStaffDepartmentReasonsDao StaffDepartmentReasonsDao
        {
            get { return Validate.Dependency(staffsepartmentReasonsDao); }
            set { staffsepartmentReasonsDao = value; }
        }

        protected IStaffNetShopIdentificationDao staffnetshopIdentificationDao;
        public IStaffNetShopIdentificationDao StaffNetShopIdentificationDao
        {
            get { return Validate.Dependency(staffnetshopIdentificationDao); }
            set { staffnetshopIdentificationDao = value; }
        }

        protected IStaffDepartmentCashDeskAvailableDao staffdepartmentCashDeskAvailableDao;
        public IStaffDepartmentCashDeskAvailableDao StaffDepartmentCashDeskAvailableDao
        {
            get { return Validate.Dependency(staffdepartmentCashDeskAvailableDao); }
            set { staffdepartmentCashDeskAvailableDao = value; }
        }

        protected IStaffDepartmentRentPlaceDao staffdepartmentRentPlaceDao;
        public IStaffDepartmentRentPlaceDao StaffDepartmentRentPlaceDao
        {
            get { return Validate.Dependency(staffdepartmentRentPlaceDao); }
            set { staffdepartmentRentPlaceDao = value; }
        }

        protected IStaffDepartmentSKB_GEDao staffdepartmentSKB_GEDao;
        public IStaffDepartmentSKB_GEDao StaffDepartmentSKB_GEDao
        {
            get { return Validate.Dependency(staffdepartmentSKB_GEDao); }
            set { staffdepartmentSKB_GEDao = value; }
        }

        protected IStaffDepartmentSoftGroupDao staffdepartmentSoftGroupDao;
        public IStaffDepartmentSoftGroupDao StaffDepartmentSoftGroupDao
        {
            get { return Validate.Dependency(staffdepartmentSoftGroupDao); }
            set { staffdepartmentSoftGroupDao = value; }
        }

        protected IStaffDepartmentInstallSoftDao staffdepartmentInstallSoftDao;
        public IStaffDepartmentInstallSoftDao StaffDepartmentInstallSoftDao
        {
            get { return Validate.Dependency(staffdepartmentInstallSoftDao); }
            set { staffdepartmentInstallSoftDao = value; }
        }

        protected IStaffDepartmentSoftGroupLinksDao staffdepartmentSoftGroupLinksDao;
        public IStaffDepartmentSoftGroupLinksDao StaffDepartmentSoftGroupLinksDao
        {
            get { return Validate.Dependency(staffdepartmentSoftGroupLinksDao); }
            set { staffdepartmentSoftGroupLinksDao = value; }
        }

        protected IStaffDepartmentBranchDao staffdepartmentBranchDao;
        public IStaffDepartmentBranchDao StaffDepartmentBranchDao
        {
            get { return Validate.Dependency(staffdepartmentBranchDao); }
            set { staffdepartmentBranchDao = value; }
        }

        protected IStaffDepartmentManagementDao staffsepartmentManagementDao;
        public IStaffDepartmentManagementDao StaffDepartmentManagementDao
        {
            get { return Validate.Dependency(staffsepartmentManagementDao); }
            set { staffsepartmentManagementDao = value; }
        }

        protected IStaffDepartmentAdministrationDao staffdepartmentAdministrationDao;
        public IStaffDepartmentAdministrationDao StaffDepartmentAdministrationDao
        {
            get { return Validate.Dependency(staffdepartmentAdministrationDao); }
            set { staffdepartmentAdministrationDao = value; }
        }

        protected IStaffDepartmentBusinessGroupDao staffdepartmentBusinessGroupDao;
        public IStaffDepartmentBusinessGroupDao StaffDepartmentBusinessGroupDao
        {
            get { return Validate.Dependency(staffdepartmentBusinessGroupDao); }
            set { staffdepartmentBusinessGroupDao = value; }
        }

        protected IStaffDepartmentRPLinkDao staffdepartmentRPLinkDao;
        public IStaffDepartmentRPLinkDao StaffDepartmentRPLinkDao
        {
            get { return Validate.Dependency(staffdepartmentRPLinkDao); }
            set { staffdepartmentRPLinkDao = value; }
        }

        protected IStaffDepartmentOperationGroupsDao staffdepartmentOperationGroupsDao;
        public IStaffDepartmentOperationGroupsDao StaffDepartmentOperationGroupsDao
        {
            get { return Validate.Dependency(staffdepartmentOperationGroupsDao); }
            set { staffdepartmentOperationGroupsDao = value; }
        }

        protected IStaffDepartmentAccessoryDao staffdepartmentAccessoryDao;
        public IStaffDepartmentAccessoryDao StaffDepartmentAccessoryDao
        {
            get { return Validate.Dependency(staffdepartmentAccessoryDao); }
            set { staffdepartmentAccessoryDao = value; }
        }

        protected IDepartmentArchiveDao departmentarchiveDao;
        public IDepartmentArchiveDao DepartmentArchiveDao
        {
            get { return Validate.Dependency(departmentarchiveDao); }
            set { departmentarchiveDao = value; }
        }

        protected IStaffWorkingConditionsDao staffworkingConditionsDao;
        public IStaffWorkingConditionsDao StaffWorkingConditionsDao
        {
            get { return Validate.Dependency(staffworkingConditionsDao); }
            set { staffworkingConditionsDao = value; }
        }

        protected IScheduleDao scheduleDao;
        public IScheduleDao ScheduleDao
        {
            get { return Validate.Dependency(scheduleDao); }
            set { scheduleDao = value; }
        }

        protected IDocumentApprovalDao documentapprovalDao;
        public IDocumentApprovalDao DocumentApprovalDao
        {
            get { return Validate.Dependency(documentapprovalDao); }
            set { documentapprovalDao = value; }
        }

        protected IStaffRequestPyrusTasksDao staffrequestPyrusTasksDao;
        public IStaffRequestPyrusTasksDao StaffRequestPyrusTasksDao
        {
            get { return Validate.Dependency(staffrequestPyrusTasksDao); }
            set { staffrequestPyrusTasksDao = value; }
        }

        protected IStaffEstablishedPostUserLinksDao staffestablishedPostUserLinksDao;
        public IStaffEstablishedPostUserLinksDao StaffEstablishedPostUserLinksDao
        {
            get { return Validate.Dependency(staffestablishedPostUserLinksDao); }
            set { staffestablishedPostUserLinksDao = value; }
        }

        protected IStaffExtraChargeActionsDao staffextraChargeActionsDao;
        public IStaffExtraChargeActionsDao StaffExtraChargeActionsDao
        {
            get { return Validate.Dependency(staffextraChargeActionsDao); }
            set { staffextraChargeActionsDao = value; }
        }

        protected IrefStaffMovementsTypesDao refstaffmovementsTypesDao;
        public IrefStaffMovementsTypesDao refStaffMovementsTypesDao
        {
            get { return Validate.Dependency(refstaffmovementsTypesDao); }
            set { refstaffmovementsTypesDao = value; }
        }

        protected IStaffLongAbsencesTypesDao stafflongAbsencesTypesDao;
        public IStaffLongAbsencesTypesDao StaffLongAbsencesTypesDao
        {
            get { return Validate.Dependency(stafflongAbsencesTypesDao); }
            set { stafflongAbsencesTypesDao = value; }
        }

        protected IStaffTemporaryReleaseVacancyRequestDao stafftemporaryReleaseVacancyRequestDao;
        public IStaffTemporaryReleaseVacancyRequestDao StaffTemporaryReleaseVacancyRequestDao
        {
            get { return Validate.Dependency(stafftemporaryReleaseVacancyRequestDao); }
            set { stafftemporaryReleaseVacancyRequestDao = value; }
        }
        #endregion
        public void SetUserRole(int roleId)
        {
            var accounts = UserDao.GetAllUserRoles(CurrentUser.Id);
            if (accounts != null)
            {
                var users = accounts.Where(x => (x.RoleId & roleId) > 0);
                int userid = 0;
                if (users != null) userid = users.First().UserId;
                if (userid > 0)
                {
                    var user = UserDao.Load(userid);
                    var dto = AuthenticationService.CreateUser(user, (UserRole)roleId);
                    AuthenticationService.setAuthTicket(dto);
                    var userLogin = new UserLogin(user) { RoleId = (int)roleId };
                    UserLoginDao.MergeAndFlush(userLogin);
                }
            }
        }
        public RouteVal GetRouteValues(int id, string type)
        {
            switch (type)
            {
                case "кп": //Кадровые перемещения
                    return new RouteVal("Edit", "StaffMovements", new { id = id });
                    break;
                case "ше": //Заявка на изменение штатной единицы
                    ///StaffList/StaffDepartmentRequest/?id={{row.entity[col.field]}}&RequestType=0&DepartmentId=0
                    var entity = StaffEstablishedPostRequestDao.Load(id);
                    return new RouteVal("StaffEstablishedPostRequest", "StaffList", new
                        {
                            RequestType = entity.RequestType.Id,
                            DepartmentId = entity.Department.Id,
                            id = entity.Id
                        });
                    break;
                default:
                    return new RouteVal("LogOn", "Account");
                    break;
            }
        }
    }
}
