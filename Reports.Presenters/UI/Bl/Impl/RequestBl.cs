using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dao.Impl;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class RequestBl : BaseBl, IRequestBl
    {
        protected string SelectAll = "Все";

        public const int VacationFirstTimesheetStatisId = 8;
        public const int VacationLastTimesheetStatisId = 12;
        public const int AbsenceFirstTimesheetStatisId = 15;
        public const int AbsenceLastTimesheetStatisId = 18;

        #region DAOs
        
        protected IVacationTypeDao vacationTypeDao;
        protected IRequestStatusDao requestStatusDao;
        protected IPositionDao positionDao;
        protected IVacationDao vacationDao;
        //protected IUserToDepartmentDao userToDepartmentDao;
        protected ITimesheetStatusDao timesheetStatusDao;
        protected IVacationCommentDao vacationCommentDao;
        protected IRequestNextNumberDao requestNextNumberDao;
        protected IRoleDao roleDao;

        protected IAbsenceTypeDao absenceTypeDao;
        protected IAbsenceDao absenceDao;
        protected IAbsenceCommentDao absenceCommentDao;

        protected ISicklistTypeDao sicklistTypeDao;
        protected ISicklistPaymentRestrictTypeDao sicklistPaymentRestrictTypeDao;
        protected ISicklistPaymentPercentDao sicklistPaymentPercentDao;
        protected ISicklistDao sicklistDao;
        protected IRequestAttachmentDao requestAttachmentDao;
        protected ISicklistCommentDao sicklistCommentDao;
        protected ISicklistBabyMindingTypeDao sicklistBabyMindingTypeDao;

        protected IHolidayWorkTypeDao holidayWorkTypeDao;
        protected IHolidayWorkDao holidayWorkDao;
        protected IHolidayWorkCommentDao holidayWorkCommentDao;

        protected IMissionTypeDao missionTypeDao;
        protected IMissionDao missionDao;
        protected IMissionCommentDao missionCommentDao;

        protected IDismissalTypeDao dismissalTypeDao;
        protected IDismissalDao dismissalDao;
        protected IDismissalCommentDao dismissalCommentDao;

        protected ITimesheetCorrectionTypeDao timesheetCorrectionTypeDao;
        protected ITimesheetCorrectionDao timesheetCorrectionDao;
        protected ITimesheetCorrectionCommentDao timesheetCorrectionCommentDao;

        protected IEmploymentTypeDao employmentTypeDao;
        protected IEmploymentHoursTypeDao employmentHoursTypeDao;
        protected IEmploymentDao employmentDao;
        protected IEmploymentCommentDao employmentCommentDao;
        protected IEmploymentAdditionDao employmentAdditionDao;

        protected IRequestPrintFormDao requestPrintFormDao;

        protected IInspectorToUserDao inspectorToUserDao;

       
        public IVacationTypeDao VacationTypeDao
        {
            get { return Validate.Dependency(vacationTypeDao); }
            set { vacationTypeDao = value; }
        }
        public IRequestStatusDao RequestStatusDao
        {
            get { return Validate.Dependency(requestStatusDao); }
            set { requestStatusDao = value; }
        }
        public IPositionDao PositionDao
        {
            get { return Validate.Dependency(positionDao); }
            set { positionDao = value; }
        }
        public IVacationDao VacationDao
        {
            get { return Validate.Dependency(vacationDao); }
            set { vacationDao = value; }
        }
        /*public IUserToDepartmentDao UserToDepartmentDao
        {
            get { return Validate.Dependency(userToDepartmentDao); }
            set { userToDepartmentDao = value; }
        }*/
        public ITimesheetStatusDao TimesheetStatusDao
        {
            get { return Validate.Dependency(timesheetStatusDao); }
            set { timesheetStatusDao = value; }
        }
        public IVacationCommentDao VacationCommentDao
        {
            get { return Validate.Dependency(vacationCommentDao); }
            set { vacationCommentDao = value; }
        }
        public IRequestNextNumberDao RequestNextNumberDao
        {
            get { return Validate.Dependency(requestNextNumberDao); }
            set { requestNextNumberDao = value; }
        }
        public IAbsenceTypeDao AbsenceTypeDao
        {
            get { return Validate.Dependency(absenceTypeDao); }
            set { absenceTypeDao = value; }
        }
        public IAbsenceDao AbsenceDao
        {
            get { return Validate.Dependency(absenceDao); }
            set { absenceDao = value; }
        }
        public IAbsenceCommentDao AbsenceCommentDao
        {
            get { return Validate.Dependency(absenceCommentDao); }
            set { absenceCommentDao = value; }
        }

        public ISicklistTypeDao SicklistTypeDao
        {
            get { return Validate.Dependency(sicklistTypeDao); }
            set { sicklistTypeDao = value; }
        }
        public ISicklistBabyMindingTypeDao SicklistBabyMindingTypeDao
        {
            get { return Validate.Dependency(sicklistBabyMindingTypeDao); }
            set { sicklistBabyMindingTypeDao = value; }
        }
        public ISicklistPaymentRestrictTypeDao SicklistPaymentRestrictTypeDao
        {
            get { return Validate.Dependency(sicklistPaymentRestrictTypeDao); }
            set { sicklistPaymentRestrictTypeDao = value; }
        }
        public ISicklistPaymentPercentDao SicklistPaymentPercentDao
        {
            get { return Validate.Dependency(sicklistPaymentPercentDao); }
            set { sicklistPaymentPercentDao = value; }
        }
        public ISicklistDao SicklistDao
        {
            get { return Validate.Dependency(sicklistDao); }
            set { sicklistDao = value; }
        }
        public IRequestAttachmentDao RequestAttachmentDao
        {
            get { return Validate.Dependency(requestAttachmentDao); }
            set { requestAttachmentDao = value; }
        }
        public ISicklistCommentDao SicklistCommentDao
        {
            get { return Validate.Dependency(sicklistCommentDao); }
            set { sicklistCommentDao = value; }
        }

        public IHolidayWorkTypeDao HolidayWorkTypeDao
        {
            get { return Validate.Dependency(holidayWorkTypeDao); }
            set { holidayWorkTypeDao = value; }
        }
        public IHolidayWorkDao HolidayWorkDao
        {
            get { return Validate.Dependency(holidayWorkDao); }
            set { holidayWorkDao = value; }
        }
        public IHolidayWorkCommentDao HolidayWorkCommentDao
        {
            get { return Validate.Dependency(holidayWorkCommentDao); }
            set { holidayWorkCommentDao = value; }
        }
        public IMissionTypeDao MissionTypeDao
        {
            get { return Validate.Dependency(missionTypeDao); }
            set { missionTypeDao = value; }
        }
        public IMissionDao MissionDao
        {
            get { return Validate.Dependency(missionDao); }
            set { missionDao = value; }
        }
        public IMissionCommentDao MissionCommentDao
        {
            get { return Validate.Dependency(missionCommentDao); }
            set { missionCommentDao = value; }
        }
        public IDismissalTypeDao DismissalTypeDao
        {
            get { return Validate.Dependency(dismissalTypeDao); }
            set { dismissalTypeDao = value; }
        }
        public IDismissalDao DismissalDao
        {
            get { return Validate.Dependency(dismissalDao); }
            set { dismissalDao = value; }
        }
        public IDismissalCommentDao DismissalCommentDao
        {
            get { return Validate.Dependency(dismissalCommentDao); }
            set { dismissalCommentDao = value; }
        }
        public ITimesheetCorrectionTypeDao TimesheetCorrectionTypeDao
        {
            get { return Validate.Dependency(timesheetCorrectionTypeDao); }
            set { timesheetCorrectionTypeDao = value; }
        }
        public ITimesheetCorrectionDao TimesheetCorrectionDao
        {
            get { return Validate.Dependency(timesheetCorrectionDao); }
            set { timesheetCorrectionDao = value; }
        }
        public ITimesheetCorrectionCommentDao TimesheetCorrectionCommentDao
        {
            get { return Validate.Dependency(timesheetCorrectionCommentDao); }
            set { timesheetCorrectionCommentDao = value; }
        }

        public IEmploymentTypeDao EmploymentTypeDao
        {
            get { return Validate.Dependency(employmentTypeDao); }
            set { employmentTypeDao = value; }
        }
        public IEmploymentHoursTypeDao EmploymentHoursTypeDao
        {
            get { return Validate.Dependency(employmentHoursTypeDao); }
            set { employmentHoursTypeDao = value; }
        }
        public IEmploymentDao EmploymentDao
        {
            get { return Validate.Dependency(employmentDao); }
            set { employmentDao = value; }
        }
        public IEmploymentCommentDao EmploymentCommentDao
        {
            get { return Validate.Dependency(employmentCommentDao); }
            set { employmentCommentDao = value; }
        }
        public IEmploymentAdditionDao EmploymentAdditionDao
        {
            get { return Validate.Dependency(employmentAdditionDao); }
            set { employmentAdditionDao = value; }
        }
        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }
        public IRequestPrintFormDao RequestPrintFormDao
        {
            get { return Validate.Dependency(requestPrintFormDao); }
            set { requestPrintFormDao = value; }
        }
        public IInspectorToUserDao InspectorToUserDao
        {
            get { return Validate.Dependency(inspectorToUserDao); }
            set { inspectorToUserDao = value; }
        }
        #endregion
        #region Create Request
        public CreateRequestModel GetCreateRequestModel(int? userId)
        {
            if(userId == null)
                userId = AuthenticationService.CurrentUser.Id;

            User user = UserDao.Load(userId.Value);
            UserRole role = (UserRole)user.Role.Id;
            CreateRequestModel model = new CreateRequestModel
            {
                IsUserVisible = role != UserRole.Employee,
                RequestTypes = GetRequestTypes()
            };
            switch(role)
            {
                case UserRole.Employee:
                    model.Users = new List<IdNameDto>{new IdNameDto(user.Id,user.FullName)};
                    break;
                case UserRole.Manager:
                case UserRole.PersonnelManager:
                    model.Users = UserDao.GetUsersForManager(user.Id, role);
                    break;


            }
            return model;
        }
        protected IList<IdNameDto> GetRequestTypes()
        {
            return new List<IdNameDto>
                       {
                           new IdNameDto((int) RequestTypeEnum.Vacation, "Заявка на отпуск"),
                           new IdNameDto((int) RequestTypeEnum.Absence, "Заявка на неявку"),
                           new IdNameDto((int) RequestTypeEnum.Sicklist, "Заявка на больничный"),
                           new IdNameDto((int) RequestTypeEnum.HolidayWork, "Заявка на оплату праздничных и выходных дней"),
                           new IdNameDto((int) RequestTypeEnum.Mission, "Заявка на командировку"),
                           new IdNameDto((int) RequestTypeEnum.Dismissal, "Заявка на увольнение"),
                           new IdNameDto((int) RequestTypeEnum.TimesheetCorrection, "Заявка на корректировку табеля"),
                           new IdNameDto((int) RequestTypeEnum.Employment, "Заявка на прием на работу")
                       }.OrderBy(x => x.Name).ToList();
        }
        #endregion

        public AllRequestListModel GetAllRequestListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            AllRequestListModel model = new AllRequestListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;   
        }
        protected void SetDictionariesToModel(AllRequestListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            //model.Types = GetEmploymentTypes(true);
            //model.GraphicTypes = GetEmploymentGraphicTypes(true);
            model.Statuses = GetRequestStatuses();
            //model.Positions = GetPositions(user);
        }
        public void SetAllRequestListModel(AllRequestListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<AllRequestDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(AllRequestListModel model, User user)
        {
            //model.Documents = new List<VacationDto>();

            List<AllRequestDto> result = new List<AllRequestDto>();
            UserRole role = (UserRole)user.Role.Id;

           
            result.AddRange(SicklistDao.GetDocuments(
                user.Id,
                role,
                0,
                0,
                0,
                model.StatusId,
                0,
                model.BeginDate,
                model.EndDate).ToList().ConvertAll(x => new AllRequestDto
                {
                    Date = x.Date,
                    EditUrl = "SicklistEdit",
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId
                }));
            result.AddRange(MissionDao.GetDocuments(
               user.Id,
               role,
               0,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   EditUrl = "MissionEdit",
                   Id = x.Id,
                   Name = x.Name,
                   UserId = x.UserId
               }));
            result.AddRange(TimesheetCorrectionDao.GetDocuments(
               user.Id,
               role,
               0,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   EditUrl = "TimesheetCorrectionEdit",
                   Id = x.Id,
                   Name = x.Name,
                   UserId = x.UserId
               }));
            result.AddRange(AbsenceDao.GetDocuments(
               user.Id,
               role,
               0,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   EditUrl = "AbsenceEdit",
                   Id = x.Id,
                   Name = x.Name,
                   UserId = x.UserId
               }));
            result.AddRange(HolidayWorkDao.GetDocuments(
              user.Id,
              role,
              0,
              0,
              0,
              model.StatusId,
              model.BeginDate,
              model.EndDate).ToList().ConvertAll(x => new AllRequestDto
              {
                  Date = x.Date,
                  EditUrl = "HolidayWorkEdit",
                  Id = x.Id,
                  Name = x.Name,
                  UserId = x.UserId
              }));
            result.AddRange(VacationDao.GetDocuments(
              user.Id,
              role,
              0,
              0,
              0,
              model.StatusId,
              model.BeginDate,
              model.EndDate).ToList().ConvertAll(x => new AllRequestDto
              {
                  Date = x.Date,
                  EditUrl = "VacationEdit",
                  Id = x.Id,
                  Name = x.Name,
                  UserId = x.UserId
              }));
            result.AddRange(EmploymentDao.GetDocuments(
               user.Id,
               role,
               0,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   EditUrl = "EmploymentEdit",
                   Id = x.Id,
                   Name = x.Name,
                   UserId = x.UserId
               }));
            result.AddRange(DismissalDao.GetDocuments(
              user.Id,
              role,
              0,
              0,
              0,
              model.StatusId,
              model.BeginDate,
              model.EndDate).ToList().ConvertAll(x => new AllRequestDto
              {
                  Date = x.Date,
                  EditUrl = "DismissalEdit",
                  Id = x.Id,
                  Name = x.Name,
                  UserId = x.UserId
              }));
            
            model.Documents = result;
        }
        #region Employment
        public EmploymentListModel GetEmploymentListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            EmploymentListModel model = new EmploymentListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetEmploymentListModel(EmploymentListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(EmploymentListModel model, User user)
        {
            //model.Documents = new List<VacationDto>();

            UserRole role = (UserRole)user.Role.Id;
            model.Documents = EmploymentDao.GetDocuments(
                user.Id,
                role,
                //model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.GraphicTypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate);
        }
        protected void SetDictionariesToModel(EmploymentListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetEmploymentTypes(true);
            model.GraphicTypes = GetEmploymentGraphicTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetEmploymentGraphicTypes(bool addAll)
        {
            var typeList = EmploymentHoursTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        protected List<IdNameDto> GetEmploymentTypes(bool addAll)
        {
            var typeList = EmploymentTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public EmploymentEditModel GetEmploymentEditModel(int id, int userId)
        {
            EmploymentEditModel model = new EmploymentEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentsToModel(model, id);
            Employment employment = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                employment = EmploymentDao.Load(id);
                if (employment == null)
                    throw new ArgumentException(string.Format("Прием на работу (id {0}) не найдена в базе данных.", id));
                model.Version = employment.Version;
                model.TypeId = employment.Type.Id;
                model.BeginDate = employment.BeginDate;
                //model.EndDate = employment.EndDate;
                model.GraphicTypeId = employment.HoursType.Id;
                model.PositionId = employment.Position.Id;
                model.AdditionId = employment.Addition == null ? 0 : employment.Addition.Id;
                model.TimesheetStatusId = employment.TimesheetStatus == null ? 0 : employment.TimesheetStatus.Id;
                model.Salary = employment.Salary.ToString();
                model.RegionFactor = GetModelValue(employment.RegionFactor);
                model.NorthFactor = GetModelValue(employment.NorthFactor);
                model.RegionAddition = GetModelValue(employment.RegionAddition);
                model.PersonalAddition = GetModelValue(employment.PersonalAddition);
                model.TravelWorkAddition = GetModelValue(employment.TravelWorkAddition);
                model.SkillAddition = GetModelValue(employment.SkillAddition);
                model.LongWorkAddition = GetModelValue(employment.LongWorkAddition);
                model.Probaion = employment.Probaion.ToString();
                model.Reason = employment.Reason;
                model.CreatorLogin = employment.Creator.Login;
                model.DocumentNumber = employment.Number.ToString();
                model.DateCreated = employment.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (employment.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, employment, model);
            LoadDictionaries(model);
            return model;
        }
        protected string GetModelValue(decimal? value)
        {
            return value.HasValue ? value.Value.ToString() : string.Empty;
        }
        public void ReloadDictionariesToModel(EmploymentEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Employment employment = EmploymentDao.Load(model.Id);
                model.CreatorLogin = employment.Creator.Login;
                model.DocumentNumber = employment.Number.ToString();
                model.DateCreated = employment.CreateDate.ToShortDateString();
            }
        }
        public bool SaveEmploymentEditModel(EmploymentEditModel model, /*UploadFilesDto filesDto,*/ out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id, true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Employment employment;
                if (model.Id == 0)
                {
                    employment = new Employment
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Employment),
                        User = user
                    };
                    ChangeEntityProperties(current, employment, model, user);
                    EmploymentDao.SaveAndFlush(employment);
                    SendEmailForUserRequest(user, current, employment.Id, employment.Number,
                                            RequestTypeEnum.Employment, false);
                    model.Id = employment.Id;
                }
                else
                {
                    employment = EmploymentDao.Load(model.Id);
                    //SaveAttachments(employment.Id, filesDto, model);
                    if (employment.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        employment.DeleteDate = DateTime.Now;
                        RequestAttachmentDao.DeleteForEntityId(model.Id);
                        EmploymentDao.SaveAndFlush(employment);
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, employment, model, user);
                        EmploymentDao.SaveAndFlush(employment);
                        if(model.Version != employment.Version)
                            SendEmailForUserRequest(user, current, employment.Id, employment.Number,
                                            RequestTypeEnum.Employment, false);
                    }
                    if (employment.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = employment.Number.ToString();
                model.Version = employment.Version;
                model.CreatorLogin = employment.Creator.Login;
                model.DateCreated = employment.CreateDate.ToShortDateString();
                SetFlagsState(employment.Id, user, employment, model);
                return true;
            }
            catch (Exception ex)
            {
                EmploymentDao.RollbackTran();
                Log.Error("Error on SaveEmploymentEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        //protected void SaveAttachments(int entityId, UploadFilesDto filesDto, EmploymentEditModel model)
        //{
        //    string fileName;
        //    int? attachmentId = SaveAttachment(entityId, model.AttachmentId, filesDto.attachment, RequestAttachmentTypeEnum.Employment , out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.AttachmentId = attachmentId.Value;
        //        model.Attachment = fileName;
        //    }
        //    attachmentId = SaveAttachment(entityId, model.PensAttachmentId, filesDto.penAttachment, RequestAttachmentTypeEnum.EmploymentPen, out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.PensAttachmentId = attachmentId.Value;
        //        model.PensAttachment = fileName;
        //    }
        //    attachmentId = SaveAttachment(entityId, model.InnAttachmentId, filesDto.innAttachment, RequestAttachmentTypeEnum.EmploymentInn, out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.InnAttachmentId = attachmentId.Value;
        //        model.InnAttachment = fileName;
        //    }
        //    attachmentId = SaveAttachment(entityId, model.NdflAttachmentId, filesDto.ndflAttachment, RequestAttachmentTypeEnum.EmploymentNdfl, out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.NdflAttachmentId = attachmentId.Value;
        //        model.NdflAttachment = fileName;
        //    }
        //}
        protected int? SaveAttachment(int entityId, int id, UploadFileDto dto,RequestAttachmentTypeEnum type, out string attachment)
        {
            attachment = string.Empty;
            if(dto == null)
                return new int?();
            RequestAttachment attach = id != 0 ?
               RequestAttachmentDao.Load(id) :
               new RequestAttachment
               {
                   RequestId = entityId,
                   RequestType = (int)type,
                   CreatorRole = RoleDao.Load((int)CurrentUser.UserRole),
               };

            attach.DateCreated = DateTime.Now;
            attach.UncompressContext = dto.Context;
            attach.ContextType = dto.ContextType;
            attach.FileName = dto.FileName;
            RequestAttachmentDao.SaveAndFlush(attach);
            attachment = attach.FileName;
            return attach.Id;
        }
        protected void ChangeEntityProperties(IUser current, Employment entity, EmploymentEditModel model, User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
// ReSharper disable PossibleInvalidOperationException
                entity.BeginDate = model.BeginDate.Value;
// ReSharper restore PossibleInvalidOperationException
                //entity.EndDate = model.EndDate;
                entity.Salary = GetTwoDigitValue(model.Salary);
                entity.RegionFactor = GetTwoDigitNullableValue(model.RegionFactor);
                entity.NorthFactor = GetTwoDigitNullableValue(model.NorthFactor);
                entity.RegionAddition = GetIntValue(model.RegionAddition);
                entity.PersonalAddition = GetIntValue(model.PersonalAddition);
                entity.TravelWorkAddition = GetIntValue(model.TravelWorkAddition);
                entity.SkillAddition = GetIntValue(model.SkillAddition);
                entity.LongWorkAddition = GetIntValue(model.LongWorkAddition);
                entity.Probaion = string.IsNullOrEmpty(model.Probaion) ? new int?() : Int32.Parse(model.Probaion); 
                entity.Type = EmploymentTypeDao.Load(model.TypeId);
                entity.HoursType = EmploymentHoursTypeDao.Load(model.GraphicTypeId);
                entity.Addition = model.AdditionId == 0 ? null : EmploymentAdditionDao.Load(model.AdditionId);
                entity.Position = PositionDao.Load(model.PositionId);
            }
        }
        protected static decimal GetTwoDigitValue(string modelValue)
        {
            return (decimal) ((int) (decimal.Parse(modelValue)*100))/100;
        }
        protected static decimal? GetTwoDigitNullableValue(string modelValue)
        {
            if(string.IsNullOrEmpty(modelValue))
                return new decimal?();
            return (decimal)((int)(decimal.Parse(modelValue) * 100)) / 100;
        }
        protected static int? GetIntValue(string modelValue)
        {
            if (string.IsNullOrEmpty(modelValue))
                return new int?();
            return int.Parse(modelValue);
        }
        protected void SetAttachmentsToModel(EmploymentEditModel model, int id)
        {
            model.AttachmentsModel = GetAttachmentsModel(id, RequestAttachmentTypeEnum.Employment);
            //if (id == 0)
            //    return;
            //RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.Employment);
            //if (attach != null)
            //{
            //    model.AttachmentId = attach.Id;
            //    model.Attachment = attach.FileName;
            //}
            //RequestAttachment attachPen = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.EmploymentPen);
            //if (attachPen != null)
            //{
            //    model.PensAttachmentId = attachPen.Id;
            //    model.PensAttachment = attachPen.FileName;
            //}
            //RequestAttachment attachInn = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.EmploymentInn);
            //if (attachInn != null)
            //{
            //    model.InnAttachmentId = attachInn.Id;
            //    model.InnAttachment = attachInn.FileName;
            //}
            //RequestAttachment attachNdfl = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.EmploymentNdfl);
            //if (attachNdfl != null)
            //{
            //    model.NdflAttachmentId = attachNdfl.Id;
            //    model.NdflAttachment = attachNdfl.FileName;
            //}
        }
        protected void LoadDictionaries(EmploymentEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Employment);
            model.AttachmentsModel = GetAttachmentsModel(model.Id, RequestAttachmentTypeEnum.Employment);
            model.TimesheetStatuses = GetTimesheetStatusesForEmployment();
            model.Types = GetEmploymentTypes(false);
            model.GraphicTypes = GetEmploymentGraphicTypes(false);
            model.Additions = GetEmploymentAdditions();
            model.Positions = GetPositionsForEmployment();
        }
        protected List<IdNameDto> GetEmploymentAdditions()
        {
            var list = EmploymentAdditionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            list.Insert(0, new IdNameDto(0, string.Empty));
            return list;
        }
        protected List<IdNameDto> GetPositionsForEmployment()
        {
            var list = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //if (addAll)
            //    typeList.Insert(0, new IdNameDto(0, SelectAll));
            return list;
        }
        protected void SetHiddenFields(EmploymentEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.AdditionIdHidden = model.AdditionId;
            model.GraphicTypeIdHidden = model.GraphicTypeId;
            model.PositionIdHidden = model.PositionId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
        }
        protected List<IdNameDto> GetTimesheetStatusesForEmployment()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, Employment entity, EmploymentEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        //model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        //model.IsPersonnelFieldsEditable = true;
                        break;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(EmploymentEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }

        #endregion
        #region Timesheet Correction
        public TimesheetCorrectionListModel GetTimesheetCorrectionListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            TimesheetCorrectionListModel model = new TimesheetCorrectionListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        protected void SetDictionariesToModel(TimesheetCorrectionListModel model, User user)
        {
            model.Departments = GetDepartments(user);
            model.Types = GetTimesheetCorrectionTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetTimesheetCorrectionTypes(bool addAll)
        {
            var typeList = TimesheetCorrectionTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetTimesheetCorrectionListModel(TimesheetCorrectionListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(TimesheetCorrectionListModel model, User user)
        {

            UserRole role = (UserRole)user.Role.Id;
            model.Documents = TimesheetCorrectionDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate);
        }
        public TimesheetCorrectionEditModel GetTimesheetCorrectionEditModel(int id, int userId)
        {
            TimesheetCorrectionEditModel model = new TimesheetCorrectionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            TimesheetCorrection timesheetCorrection = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                timesheetCorrection = TimesheetCorrectionDao.Load(id);
                if (timesheetCorrection == null)
                    throw new ArgumentException(string.Format("Корректировка табеля (id {0}) не найдена в базе данных.", id));
                model.Version = timesheetCorrection.Version;
                model.TypeId = timesheetCorrection.Type.Id;
                model.EventDate = timesheetCorrection.EventDate;
                //model.Compensation = timesheetCorrection.Compensation.HasValue ? timesheetCorrection.Compensation.Value.ToString() : string.Empty;
                model.StatusId = timesheetCorrection.TimesheetStatus == null ? 0 : timesheetCorrection.TimesheetStatus.Id;
                model.Hours = timesheetCorrection.Hours.ToString();
                model.CreatorLogin = timesheetCorrection.Creator.Login;
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.DateCreated = timesheetCorrection.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (timesheetCorrection.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, timesheetCorrection, model);
            LoadDictionaries(model);
            return model;
        }
        protected void LoadDictionaries(TimesheetCorrectionEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.TimesheetCorrection);
            model.Statuses = GetTimesheetStatusesForTimesheetCorrection();
            model.Types = GetTimesheetCorrectionTypes(false);
        }
        protected void SetHiddenFields(TimesheetCorrectionEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.StatusIdHidden = model.StatusId;
        }
        protected List<IdNameDto> GetTimesheetStatusesForTimesheetCorrection()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, TimesheetCorrection entity, TimesheetCorrectionEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsStatusEditable = true;
                        //model.IsPersonnelFieldsEditable = true;
                        break;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsStatusEditable = true;
                            //model.IsPersonnelFieldsEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(TimesheetCorrectionEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsStatusEditable = state;
            model.IsTypeEditable = state;
            //model.IsPersonnelFieldsEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }
        public bool SaveTimesheetCorrectionEditModel(TimesheetCorrectionEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                TimesheetCorrection timesheetCorrection;
                if (model.Id == 0)
                {
                    timesheetCorrection = new TimesheetCorrection
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.TimesheetCorrection),
                        User = user
                    };
                    ChangeEntityProperties(current, timesheetCorrection, model, user);
                    TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                    model.Id = timesheetCorrection.Id;
                }
                else
                {
                    timesheetCorrection = TimesheetCorrectionDao.Load(model.Id);
                    if (timesheetCorrection.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        timesheetCorrection.DeleteDate = DateTime.Now;
                        TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, timesheetCorrection, model, user);
                        TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                    }
                    if (timesheetCorrection.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.Version = timesheetCorrection.Version;
                //model.DaysCount = dismissal.DaysCount;
                model.CreatorLogin = timesheetCorrection.Creator.Login;
                model.DateCreated = timesheetCorrection.CreateDate.ToShortDateString();
                SetFlagsState(timesheetCorrection.Id, user, timesheetCorrection, model);
                return true;
            }
            catch (Exception ex)
            {
                TimesheetCorrectionDao.RollbackTran();
                Log.Error("Error on SaveTimesheetCorrectionEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, TimesheetCorrection entity, TimesheetCorrectionEditModel model, User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                //entity.Compensation = string.IsNullOrEmpty(model.Compensation) ? new decimal?() : (decimal)((int)(decimal.Parse(model.Compensation) * 100)) / 100;
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
// ReSharper disable PossibleInvalidOperationException
                entity.EventDate = model.EventDate.Value;
// ReSharper restore PossibleInvalidOperationException
                entity.Hours = Int32.Parse(model.Hours);
                entity.Type = TimesheetCorrectionTypeDao.Load(model.TypeId);
            }
        }
        public void ReloadDictionariesToModel(TimesheetCorrectionEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                TimesheetCorrection timesheetCorrection = TimesheetCorrectionDao.Load(model.Id);
                model.CreatorLogin = timesheetCorrection.Creator.Login;
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.DateCreated = timesheetCorrection.CreateDate.ToShortDateString();
            }
        }
        #endregion
        #region Dismissal
        public DismissalListModel GetDismissalListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            DismissalListModel model = new DismissalListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetDismissalListModel(DismissalListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(DismissalListModel model, User user)
        {

            UserRole role = (UserRole)user.Role.Id;
            model.Documents = DismissalDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate);
        }
        protected void SetDictionariesToModel(DismissalListModel model, User user)
        {
            model.Departments = GetDepartments(user);
            model.Types = GetDismissalTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetDismissalTypes(bool addAll)
        {
            var typeList = DismissalTypeDao.LoadAll().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name+" "+(x.Reason.Length > 64?x.Reason.Substring(0,64)+" ...":x.Reason))).OrderBy(x =>x.Name).ToList();
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }

        public DismissalEditModel GetDismissalEditModel(int id, int userId)
        {
            DismissalEditModel model = new DismissalEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id,RequestAttachmentTypeEnum.Dismissal);
            Dismissal dismissal = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                dismissal = DismissalDao.Load(id);
                if (dismissal == null)
                    throw new ArgumentException(string.Format("Командировка (id {0}) не найдена в базе данных.", id));
                model.Version = dismissal.Version;
                model.TypeId = dismissal.Type == null? 0 : dismissal.Type.Id;
                model.EndDate = dismissal.EndDate;
                model.Compensation = dismissal.Compensation.HasValue? dismissal.Compensation.Value.ToString():string.Empty;
                //model.StatusId = dismissal.TimesheetStatus == null ? 0 : dismissal.TimesheetStatus.Id;
                model.Reason = dismissal.Reason;
                model.CreatorLogin = dismissal.Creator.Login;
                model.DocumentNumber = dismissal.Number.ToString();
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (dismissal.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, dismissal, model);
            LoadDictionaries(model);
            return model;
        }
        protected void LoadDictionaries(DismissalEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Dismissal);
            //model.Statuses = GetTimesheetStatusesForDismissal();
            model.Types = GetDismissalTypes(false);
        }
        protected void SetHiddenFields(DismissalEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            //model.StatusIdHidden = model.StatusId;
            //model.DaysCountHidden = model.DaysCount;
        }
        protected List<IdNameDto> GetTimesheetStatusesForDismissal()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, Dismissal entity, DismissalEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = false;
                        //model.IsStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        //model.IsStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
                        break;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    RequestPrintForm form = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.Dismissal);
                    model.IsPrintAvailable = form != null;
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    RequestPrintForm formMan = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.Dismissal);
                    model.IsPrintAvailable = formMan != null;
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            //model.IsStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                  
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            //model.IsStatusEditable = true;
                            model.IsPersonnelFieldsEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable /*|| model.IsStatusEditable*/
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable || model.IsPersonnelFieldsEditable;
        }
        protected void SetFlagsState(DismissalEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            //model.IsStatusEditable = state;
            model.IsTypeEditable = state;
            model.IsPersonnelFieldsEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
            model.IsPrintAvailable = state;
        }
        public bool SaveDismissalEditModel(DismissalEditModel model, UploadFileDto fileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Dismissal dismissal;
                if (model.Id == 0)
                {
                    dismissal = new Dismissal
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Dismissal),
                        User = user
                    };
                    ChangeEntityProperties(current, dismissal, model, user);
                    DismissalDao.SaveAndFlush(dismissal);
                    model.Id = dismissal.Id;
                }
                else
                {
                    dismissal = DismissalDao.Load(model.Id);
                    string fileName;
                    int? attachmentId = SaveAttachment(dismissal.Id, model.AttachmentId, fileDto, RequestAttachmentTypeEnum.Dismissal, out fileName);
                    if (attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }

                    if (dismissal.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;

                        dismissal.DeleteDate = DateTime.Now;
                        DismissalDao.SaveAndFlush(dismissal);
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, dismissal, model, user);
                        DismissalDao.SaveAndFlush(dismissal);
                    }
                    if (dismissal.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = dismissal.Number.ToString();
                model.Version = dismissal.Version;
                //model.DaysCount = dismissal.DaysCount;
                model.CreatorLogin = dismissal.Creator.Login;
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
                SetFlagsState(dismissal.Id, user, dismissal, model);
                return true;
            }
            catch (Exception ex)
            {
                DismissalDao.RollbackTran();
                Log.Error("Error on SaveDismissalEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, Dismissal entity, DismissalEditModel model, User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                //entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.Reason = model.Reason;
                entity.Type = DismissalTypeDao.Load(model.TypeId);
                //entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                entity.Compensation = string.IsNullOrEmpty(model.Compensation)?new decimal?() : (decimal)((int)(decimal.Parse(model.Compensation) * 100)) / 100;
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
// ReSharper disable PossibleInvalidOperationException
                entity.EndDate = model.EndDate.Value;
// ReSharper restore PossibleInvalidOperationException
                
            }
        }
        public void ReloadDictionariesToModel(DismissalEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Dismissal dismissal = DismissalDao.Load(model.Id);
                model.CreatorLogin = dismissal.Creator.Login;
                model.DocumentNumber = dismissal.Number.ToString();
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
            }
        }
        #endregion
        #region Mission
        public MissionListModel GetMissionListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            MissionListModel model = new MissionListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetMissionListModel(MissionListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        protected void SetDictionariesToModel(MissionListModel model, User user)
        {
            model.Departments = GetDepartments(user);
            model.Types = GetMissionTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetMissionTypes(bool addAll)
        {
            var typeList = MissionTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetDocumentsToModel(MissionListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = MissionDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate);
        }
        public MissionEditModel GetMissionEditModel(int id, int userId)
        {
            MissionEditModel model = new MissionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            Mission mission = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                mission = MissionDao.Load(id);
                if (mission == null)
                    throw new ArgumentException(string.Format("Командировка (id {0}) не найдена в базе данных.", id));
                model.Version = mission.Version;
                model.TypeId = mission.Type.Id;
                model.BeginDate = mission.BeginDate;
                model.EndDate = mission.EndDate;
                model.DaysCount = mission.DaysCount;
                model.TimesheetStatusId = mission.TimesheetStatus == null ? 0 : mission.TimesheetStatus.Id;
                model.Country = mission.Country;
                model.MissionOrganization = mission.Organization;
                model.Goal = mission.Goal;
                model.FinancesSource = mission.FinancesSource;
                model.Reason = mission.Reason;
                model.CreatorLogin = mission.Creator.Login;
                model.DocumentNumber = mission.Number.ToString();
                model.DateCreated = mission.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (mission.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, mission, model);
            LoadDictionaries(model);
            return model;
        }
        protected void SetFlagsState(int id, User user, Mission entity, MissionEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        model.IsReasonEditable = true;
                        break;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                            model.IsReasonEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable || model.IsReasonEditable;
        }
        protected void SetFlagsState(MissionEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;
            model.IsReasonEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }
        protected void LoadDictionaries(MissionEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Mission);
            model.TimesheetStatuses = GetTimesheetStatusesForMission();
            model.Types = GetMissionTypes(false);
        }
        protected List<IdNameDto> GetTimesheetStatusesForMission()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id == 7)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetHiddenFields(MissionEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
            model.DaysCountHidden = model.DaysCount;
        }
        public bool SaveMissionEditModel(MissionEditModel model,out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Mission mission;
                if (model.Id == 0)
                {
                    mission = new Mission
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Mission),
                        User = user
                    };
                    ChangeEntityProperties(current, mission, model, user);
                    MissionDao.SaveAndFlush(mission);
                    model.Id = mission.Id;
                }
                else
                {
                    mission = MissionDao.Load(model.Id);
                    if (mission.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        mission.DeleteDate = DateTime.Now;
                        MissionDao.SaveAndFlush(mission);
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, mission, model, user);
                        MissionDao.SaveAndFlush(mission);
                    }
                    if (mission.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = mission.Number.ToString();
                model.Version = mission.Version;
                model.DaysCount = mission.DaysCount;
                model.CreatorLogin = mission.Creator.Login;
                model.DateCreated = mission.CreateDate.ToShortDateString();
                SetFlagsState(mission.Id, user, mission, model);
                return true;
            }
            catch (Exception ex)
            {
                MissionDao.RollbackTran();
                Log.Error("Error on SaveMissionEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        public void ReloadDictionariesToModel(MissionEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Mission mission = MissionDao.Load(model.Id);
                model.CreatorLogin = mission.Creator.Login;
                model.DocumentNumber = mission.Number.ToString();
                model.DateCreated = mission.CreateDate.ToShortDateString();
            }
        }
        protected void ChangeEntityProperties(IUser current, Mission entity, MissionEditModel model, User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                entity.Reason = model.Reason;
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
// ReSharper disable PossibleInvalidOperationException
                entity.BeginDate = model.BeginDate.Value;
                entity.EndDate = model.EndDate.Value;
// ReSharper restore PossibleInvalidOperationException
                entity.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                entity.Country = model.Country;
                entity.Organization = model.MissionOrganization;
                entity.Goal = model.Goal;
                entity.FinancesSource = model.FinancesSource;
                //entity.Reason = model.Reason;
                entity.Type = MissionTypeDao.Load(model.TypeId);
            }
        }
        #endregion
        #region HolidayWork
        public HolidayWorkListModel GetHolidayWorkListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            HolidayWorkListModel model = new HolidayWorkListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetHolidayWorkListModel(HolidayWorkListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(HolidayWorkListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = HolidayWorkDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate
                //model.PaymentPercentType
                );
        }
        protected void SetDictionariesToModel(HolidayWorkListModel model, User user)
        {
            model.Departments = GetDepartments(user);
            model.Types = GetHolidayWorkTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetHolidayWorkTypes(bool addAll)
        {
            var typeList = HolidayWorkTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public HolidayWorkEditModel GetHolidayWorkEditModel(int id, int userId)
        {
            HolidayWorkEditModel model = new HolidayWorkEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            HolidayWork holidayWork = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                holidayWork = HolidayWorkDao.Load(id);
                if (holidayWork == null)
                    throw new ArgumentException(string.Format("Больничный (id {0}) не найдена в базе данных.", id));
                model.Version = holidayWork.Version;
                model.TypeId = holidayWork.Type.Id;
                model.Date = holidayWork.WorkDate;
                model.TimesheetStatusId = holidayWork.TimesheetStatus == null ? 0 : holidayWork.TimesheetStatus.Id;
                //model.Rate = holidayWork.Rate.ToString();
                model.Hours = holidayWork.Hours.ToString();
                model.CreatorLogin = holidayWork.Creator.Login;
                model.DocumentNumber = holidayWork.Number.ToString();
                model.DateCreated = holidayWork.CreateDate.ToShortDateString();

                SetHiddenFields(model);
                if (holidayWork.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, holidayWork, model);
            LoadDictionaries(model);
            return model;
        }
        public bool SaveHolidayWorkEditModel(HolidayWorkEditModel model, /*UploadFileDto fileDto,*/ out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                HolidayWork holidayWork;
                if (model.Id == 0)
                {
                    holidayWork = new HolidayWork
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.HolidayWork),
                        User = user
                    };
                    ChangeEntityProperties(current, holidayWork, model, user);
                    HolidayWorkDao.SaveAndFlush(holidayWork);
                    model.Id = holidayWork.Id;
                }
                else
                {
                    holidayWork = HolidayWorkDao.Load(model.Id);
                    //SaveAttachment(holidayWork, fileDto, model, RequestTypeEnum.Sicklist);
                    if (holidayWork.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        //if (model.AttachmentId > 0)
                        //    RequestAttachmentDao.Delete(model.AttachmentId);
                        holidayWork.DeleteDate = DateTime.Now;
                        HolidayWorkDao.SaveAndFlush(holidayWork);
                        model.IsDelete = false;
                        //model.AttachmentId = 0;
                        //model.Attachment = string.Empty;
                    }
                    else
                    {
                        ChangeEntityProperties(current, holidayWork, model, user);
                        HolidayWorkDao.SaveAndFlush(holidayWork);
                    }
                    if (holidayWork.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = holidayWork.Number.ToString();
                model.Version = holidayWork.Version;
                //model.DaysCount = holidayWork.DaysCount;
                model.CreatorLogin = holidayWork.Creator.Login;
                model.DateCreated = holidayWork.CreateDate.ToShortDateString();
                SetFlagsState(holidayWork.Id, user, holidayWork, model);
                return true;
            }
            catch (Exception ex)
            {
                HolidayWorkDao.RollbackTran();
                Log.Error("Error on SaveHolidayWorkEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, HolidayWork entity, HolidayWorkEditModel model, User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
// ReSharper disable PossibleInvalidOperationException
                entity.WorkDate = model.Date.Value;
// ReSharper restore PossibleInvalidOperationException
                entity.Hours = Int32.Parse(model.Hours);
                //entity.Rate = Int32.Parse(model.Rate);
                entity.Type = HolidayWorkTypeDao.Load(model.TypeId);
            }
        }
        protected void SetFlagsState(int id, User user, HolidayWork entity, HolidayWorkEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(HolidayWorkEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }
        protected void SetHiddenFields(HolidayWorkEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
        }
        protected void LoadDictionaries(HolidayWorkEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.HolidayWork);
            model.TimesheetStatuses = GetTimesheetStatusesForHolidayWork();
            model.Types = GetHolidayWorkTypes(false);
        }
        protected List<IdNameDto> GetTimesheetStatusesForHolidayWork()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id == 6) || (x.Id == 13)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        public void ReloadDictionariesToModel(HolidayWorkEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                HolidayWork holidayWork = HolidayWorkDao.Load(model.Id);
                model.CreatorLogin = holidayWork.Creator.Login;
                model.DocumentNumber = holidayWork.Number.ToString();
                model.DateCreated = holidayWork.CreateDate.ToShortDateString();
            }
        }
        #endregion
        #region Sicklist
        public SicklistListModel GetSicklistListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            SicklistListModel model = new SicklistListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                Department = new IdNameDto { Id = 0,Name = string.Empty},
            };
            SetDictionariesToModel(model,user);
            return model;
        }

        protected List<IdNameDto> GetSicklistTypes(bool addAll,bool addEmpty)
        {
            var typeList = SicklistTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            else if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }
        protected List<IdNameDto> GetBabyMindingTypes(bool addAll)
        {
            var typeList = SicklistBabyMindingTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        protected List<IdNameDtoSort> GetSicklisPaymentPercentTypes(bool addAll,bool addNameAll)
        {
            List<IdNameDtoSort> typeList = SicklistPaymentPercentDao.LoadAll().ToList().
                ConvertAll(
                    x =>
                    new IdNameDtoSort
                        {
                            Id = x.Id, 
                            Name = x.SicklistPercent.ToString() + "%",
                            SortOrder = x.SortOrder
                        }).OrderBy(x => x.SortOrder).ToList();
            if (addAll)
                typeList.Insert(0,
                                new IdNameDtoSort
                                    {
                                        Id = 0,
                                        Name = addNameAll ? SelectAll : string.Empty,
                                        SortOrder = -100
                                    });
            return typeList;
        }
        protected List<IdNameDto> GetSicklisPaymentRestrictTypes(bool addEmpty)
        {
            var typeList = SicklistPaymentRestrictTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if(addEmpty)
                typeList.Insert(0, new IdNameDto(0, "Отсутствует"));
            return typeList;
        }
        public void SetSicklistListModel(SicklistListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        protected void SetDictionariesToModel(SicklistListModel model, User user)
        {
            //model.Department = GetDepartments(user);
            model.Types = GetSicklistTypes(true,false);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
            model.PaymentPercentTypes = GetSicklisPaymentPercentTypes(true,true);
       }
        public void SetDocumentsToModel(SicklistListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = SicklistDao.GetDocuments(
                user.Id,
                role,
                GetDepartmentId(model.Department),
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.PaymentPercentType,
                model.BeginDate,
                model.EndDate);
        }
        public SicklistEditModel GetSicklistEditModel(int id, int userId)
        {
            SicklistEditModel model = new SicklistEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id,RequestAttachmentTypeEnum.Sicklist);
            Sicklist sicklist = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                sicklist = SicklistDao.Load(id);
                if (sicklist == null)
                    throw new ArgumentException(string.Format("Больничный (id {0}) не найдена в базе данных.", id));
                model.Version = sicklist.Version;
                model.TypeId = sicklist.Type == null? 0 : sicklist.Type.Id;
                model.BabyMindingTypeId = sicklist.BabyMindingType == null ? new int?() : sicklist.BabyMindingType.Id;
                model.BeginDate = sicklist.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = sicklist.EndDate;
                model.TimesheetStatusId = sicklist.TimesheetStatus == null ? 0 : sicklist.TimesheetStatus.Id;
                model.DaysCount = sicklist.DaysCount;
                model.CreatorLogin = sicklist.Creator.Login;
                model.DocumentNumber = sicklist.Number.ToString();
                model.DateCreated = sicklist.CreateDate.ToShortDateString();

                model.PaymentBeginDate = sicklist.PaymentBeginDate;
                model.ExperienceYears = !sicklist.ExperienceYears.HasValue || sicklist.ExperienceYears.Value == 0 ? 
                                        string.Empty:sicklist.ExperienceYears.Value.ToString();
                model.ExperienceMonthes = !sicklist.ExperienceMonthes.HasValue || sicklist.ExperienceMonthes.Value == 0 ?
                                        string.Empty : sicklist.ExperienceMonthes.Value.ToString();
                model.PaymentPercentTypeId = sicklist.PaymentPercent == null ? 0 : sicklist.PaymentPercent.Id;
                model.PaymentRestrictTypeId = sicklist.RestrictType == null ? 0 : sicklist.RestrictType.Id;
                model.PaymentDecreaseDate = sicklist.PaymentDecreaseDate;
                model.IsPreviousPaymentCounted = sicklist.IsPreviousPaymentCounted;
                model.Is2010Calculate = sicklist.Is2010Calculate;
                model.IsAddToFullPayment = sicklist.IsAddToFullPayment;
                SetHiddenFields(model);
                if (sicklist.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, sicklist, model);
            LoadDictionaries(model);
            return model;
        }
        protected void SetHiddenFields(SicklistEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.BabyMindingTypeIdHidden = model.BabyMindingTypeId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
            model.DaysCountHidden = model.DaysCount;
            model.PaymentPercentTypeIdHidden = model.PaymentPercentTypeId;
            model.PaymentRestrictTypeIdHidden = model.PaymentRestrictTypeId;
            model.Is2010CalculateHidden = model.Is2010Calculate;
            model.IsPreviousPaymentCountedHidden = model.IsPreviousPaymentCounted;
            model.IsAddToFullPaymentHidden = model.IsAddToFullPaymentHidden;
        }
        protected void SetAttachmentToModel(IAttachment model, int id,RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null) 
                return;
            model.AttachmentId = attach.Id;
            model.Attachment = attach.FileName;
        }
        public bool SaveSicklistEditModel(SicklistEditModel model,UploadFileDto fileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Sicklist sicklist;
                if (model.Id == 0)
                {
                    sicklist = new Sicklist
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Sicklist),
                        User = user
                    };
                    ChangeEntityProperties(current, sicklist, model, user);
                    SicklistDao.SaveAndFlush(sicklist);
                    model.Id = sicklist.Id;
                }
                else
                {
                    sicklist = SicklistDao.Load(model.Id);
                    string fileName;
                    int? attachmentId = SaveAttachment(sicklist.Id,model.AttachmentId ,fileDto,RequestAttachmentTypeEnum.Sicklist,out fileName);
                    if(attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }
                    if (sicklist.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        sicklist.DeleteDate = DateTime.Now;
                        SicklistDao.SaveAndFlush(sicklist);
                        model.IsDelete = false;
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                    }
                    else
                    {
                        ChangeEntityProperties(current,sicklist,model,user);   
                        SicklistDao.SaveAndFlush(sicklist);
                    }
                    if (sicklist.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = sicklist.Number.ToString();
                model.Version = sicklist.Version;
                model.DaysCount = sicklist.DaysCount;
                model.CreatorLogin = sicklist.Creator.Login;
                model.DateCreated = sicklist.CreateDate.ToShortDateString();
                SetFlagsState(sicklist.Id, user, sicklist, model);
                //throw new ArgumentException("Test exception");
                return true;
            }
            catch (Exception ex)
            {
                SicklistDao.RollbackTran();
                Log.Error("Error on SaveSicklistEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        //protected void SaveAttachment(int entityId, UploadFileDto fileDto,SicklistEditModel model, RequestAttachmentTypeEnum type)
        //{
        //    if (fileDto == null)
        //        return;
        //    RequestAttachment attach = model.AttachmentId != 0 ?
        //        RequestAttachmentDao.Load(model.AttachmentId) :
        //        new RequestAttachment
        //            {
        //                RequestId = entityId,
        //                RequestType = (int) type,
        //            };

        //    attach.DateCreated = DateTime.Now;
        //    attach.UncompressContext = fileDto.Context;
        //    attach.ContextType = fileDto.ContextType;
        //    attach.FileName = fileDto.FileName;
        //    RequestAttachmentDao.SaveAndFlush(attach);
        //    model.AttachmentId = attach.Id;
        //    model.Attachment = attach.FileName;
        //}
       
        protected void ChangeEntityProperties(IUser current, Sicklist sicklist,SicklistEditModel model,User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !sicklist.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                sicklist.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !sicklist.ManagerDateAccept.HasValue)
            {
                if (model.IsApprovedByUser && !sicklist.UserDateAccept.HasValue)
                    sicklist.UserDateAccept = DateTime.Now;
                sicklist.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                    sicklist.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !sicklist.PersonnelManagerDateAccept.HasValue)
            {
                if (model.IsApprovedByUser && !sicklist.UserDateAccept.HasValue)
                    sicklist.UserDateAccept = DateTime.Now;
                if (model.IsPersonnelFieldsEditable)
                    SetPersonnelDataFromModel(sicklist, model);
                if (model.IsApprovedByPersonnelManager)
                    sicklist.PersonnelManagerDateAccept = DateTime.Now;
            }
            if(model.IsDatesEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                sicklist.BeginDate = model.BeginDate.Value;
                sicklist.EndDate = model.EndDate.Value;
                // ReSharper restore PossibleInvalidOperationException
                sicklist.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
            }
            if (model.IsTypeEditable)
            {
                sicklist.Type = SicklistTypeDao.Load(model.TypeId);
                if (model.TypeId == sicklistTypeDao.SicklistTypeIdBabyMinding)
                    sicklist.BabyMindingType = model.BabyMindingTypeId.HasValue 
                        ? SicklistBabyMindingTypeDao.Load(model.BabyMindingTypeId.Value)
                        : null;
                else
                    sicklist.BabyMindingType = null;
            }
        }
        protected void SetPersonnelDataFromModel(Sicklist sicklist,SicklistEditModel model)
        {
            sicklist.PaymentBeginDate = model.PaymentBeginDate;
            sicklist.ExperienceYears = GetIntFromModel(model.ExperienceYears);
            sicklist.ExperienceMonthes = GetIntFromModel(model.ExperienceMonthes); 
            sicklist.PaymentPercent = model.PaymentPercentTypeId == 0 ? null : SicklistPaymentPercentDao.Load(model.PaymentPercentTypeId);
            sicklist.RestrictType = model.PaymentRestrictTypeId == 0 ? null : SicklistPaymentRestrictTypeDao.Load(model.PaymentRestrictTypeId);
            sicklist.PaymentDecreaseDate = model.PaymentDecreaseDate;
            sicklist.IsPreviousPaymentCounted = model.IsPreviousPaymentCounted;
            sicklist.Is2010Calculate = model.Is2010Calculate;
            sicklist.IsAddToFullPayment = model.IsAddToFullPayment;
        }
        protected int? GetIntFromModel(string modelValue)
        {
            if (!string.IsNullOrEmpty(modelValue))
            {
                int experienceYears;
                if (Int32.TryParse(modelValue, out experienceYears))
                    return experienceYears;
                throw new ArgumentException("Значение поля не является целым числом."); 
            }
            return null;
        }
        public void ReloadDictionariesToModel(SicklistEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Sicklist sicklist = SicklistDao.Load(model.Id);
                model.CreatorLogin = sicklist.Creator.Login;
                model.DocumentNumber = sicklist.Number.ToString();
                model.DateCreated = sicklist.CreateDate.ToShortDateString();
            }
        }
        protected void LoadDictionaries(SicklistEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Sicklist);
            model.TimesheetStatuses = GetTimesheetStatusesForSicklist();
            model.Types = GetSicklistTypes(false,false);
            model.PaymentPercentTypes = GetSicklisPaymentPercentTypes(!model.IsPersonnelFieldsEditable,false);
            model.PaymentRestrictTypes = GetSicklisPaymentRestrictTypes(true);
            model.BabyMindingTypes = GetBabyMindingTypes(false);
            model.SicklistTypeIdBabyMindingHidden = SicklistTypeDao.SicklistTypeIdBabyMinding;
        }
        protected List<IdNameDto> GetTimesheetStatusesForSicklist()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id == 2) || (x.Id == 3) || (x.Id == 11) || (x.Id == 12)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, Sicklist entity, SicklistEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsBabyMindingTypeEditable = false;
                model.IsDatesEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
                        model.IsTypeEditable = true;
                        break;
                }
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsDatesEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsDatesEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                            model.IsPersonnelFieldsEditable = true;
                            model.IsDatesEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }

            model.IsBabyMindingTypeEditable = model.IsTypeEditable && (model.TypeId == SicklistTypeDao.SicklistTypeIdBabyMinding);
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable || model.IsPersonnelFieldsEditable || model.IsDatesEditable;
        }
        protected void SetFlagsState(SicklistEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;
            model.IsBabyMindingTypeEditable = state;
            model.IsDatesEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;

            model.IsPersonnelFieldsEditable = state;
        }
        #endregion
        #region Absence
        public AbsenceListModel GetAbsenceListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            AbsenceListModel model = new AbsenceListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                Departments = GetDepartments(user),
                AbsenceTypes = GetAbsenceTypes(true),
                RequestStatuses = GetRequestStatuses(),
                Positions = GetPositions(user)
            };
            return model;
        }
        protected List<IdNameDto> GetAbsenceTypes(bool addAll)
        {
            var typeList = AbsenceTypeDao.LoadAllSorted().
                Where(x => x.Code.CompareTo("55") == 0 || x.Code.CompareTo("56") == 0).
                ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetAbsenceListModel(AbsenceListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
            model.AbsenceTypes = GetAbsenceTypes(true);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(AbsenceListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = AbsenceDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.PositionId,
                model.AbsenceTypeId,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate);
        }
        public AbsenceEditModel GetAbsenceEditModel(int id, int userId)
        {
            AbsenceEditModel model = new AbsenceEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.Absence);
            model.TimesheetStatuses = GetTimesheetStatusesForAbsence();
            model.AbsenceTypes = GetAbsenceTypes(false);
            Absence absence = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                absence = AbsenceDao.Load(id);
                if (absence == null)
                    throw new ArgumentException(string.Format("Заявка на неявку (id {0}) не найдена в базе данных.", id));
                model.Version = absence.Version;
                model.AbsenceTypeId = absence.Type.Id;
                model.AbsenceTypeIdHidden = model.AbsenceTypeId;
                model.BeginDate = absence.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = absence.EndDate;
                model.TimesheetStatusId = absence.TimesheetStatus == null ? 0 : absence.TimesheetStatus.Id;
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.DaysCount = absence.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                model.CreatorLogin = absence.Creator.Login;
                model.DocumentNumber = absence.Number.ToString();
                model.DateCreated = absence.CreateDate.ToShortDateString();
                if (absence.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id,user,absence,model);
            return model;
        }
        protected List<IdNameDto> GetTimesheetStatusesForAbsence()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id >= AbsenceFirstTimesheetStatisId) && (x.Id <= AbsenceLastTimesheetStatisId)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x=>x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, Absence absence, AbsenceEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsAbsenceTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                }
                if(currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser  = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = absence.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = absence.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = absence.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = absence.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!absence.UserDateAccept.HasValue && !absence.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        //model.IsSaveAvailable = true;
                        if (!absence.ManagerDateAccept.HasValue && !absence.PersonnelManagerDateAccept.HasValue && !absence.SendTo1C.HasValue)
                            model.IsAbsenceTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!absence.ManagerDateAccept.HasValue && !absence.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!absence.PersonnelManagerDateAccept.HasValue && !absence.SendTo1C.HasValue)
                        {
                            model.IsAbsenceTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!absence.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!absence.SendTo1C.HasValue)
                        {
                            model.IsAbsenceTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!absence.SendTo1C.HasValue && !absence.DeleteDate.HasValue)
                            model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsAbsenceTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(AbsenceEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsAbsenceTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }
        public void ReloadDictionariesToModel(AbsenceEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Absence);
            model.TimesheetStatuses = GetTimesheetStatusesForAbsence();
            model.AbsenceTypes = GetAbsenceTypes(false);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Absence absence = AbsenceDao.Load(model.Id);
                model.CreatorLogin = absence.Creator.Login;
                model.DocumentNumber = absence.Number.ToString();
                model.DateCreated = absence.CreateDate.ToShortDateString();
                model.DaysCount = absence.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                if (absence.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
        }
        public bool SaveAbsenceEditModel(AbsenceEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Absence absence;
                if (model.Id == 0)
                {
                    absence = new Absence
                    {   
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
// ReSharper disable PossibleInvalidOperationException
                        BeginDate = model.BeginDate.Value,
                        EndDate = model.EndDate.Value,
// ReSharper restore PossibleInvalidOperationException
                        DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1,
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Absence),
                        //Status = RequestStatusDao.Load((int)RequestStatusEnum.NotApproved),
                        Type = AbsenceTypeDao.Load(model.AbsenceTypeId),
                        User = user
                    };
                    if (current.UserRole == UserRole.Employee && current.Id == model.UserId && model.IsApprovedByUser)
                        absence.UserDateAccept = DateTime.Now;
                    if (current.UserRole == UserRole.Manager && user.Manager != null
                        && current.Id == user.Manager.Id)
                    {
                        if (model.IsApprovedByUser && !absence.UserDateAccept.HasValue)
                            absence.UserDateAccept = DateTime.Now;
                        absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if(model.IsApprovedByManager)
                            absence.ManagerDateAccept = DateTime.Now;
                    }
                    if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                        && current.Id == user.PersonnelManager.Id)
                    {
                        if (model.IsApprovedByUser && !absence.UserDateAccept.HasValue)
                            absence.UserDateAccept = DateTime.Now;
                        absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApprovedByPersonnelManager)
                            absence.PersonnelManagerDateAccept = DateTime.Now;
                    }
                    AbsenceDao.SaveAndFlush(absence);
                    model.Id = absence.Id;
                }
                else
                {
                    absence = AbsenceDao.Load(model.Id);
                    if (absence.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        absence.DeleteDate = DateTime.Now;
                        AbsenceDao.SaveAndFlush(absence);
                        //model.TimesheetStatusId = absence.TimesheetStatus == null? 0:absence.TimesheetStatus.Id;
                        //model.AbsenceTypeId = absence.Type.Id;
                        model.IsDelete = false;
                    }
                    else
                    {
                        if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                            && !absence.UserDateAccept.HasValue
                            && model.IsApprovedByUser)
                            absence.UserDateAccept = DateTime.Now;
                        if (current.UserRole == UserRole.Manager && user.Manager != null
                            && current.Id == user.Manager.Id
                            && !absence.ManagerDateAccept.HasValue)
                        {
                            absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if(model.IsApprovedByManager)
                                absence.ManagerDateAccept = DateTime.Now;
                        }
                        if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                            && current.Id == user.PersonnelManager.Id
                            && !absence.PersonnelManagerDateAccept.HasValue
                            )
                        {
                            absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (model.IsApprovedByPersonnelManager)
                                absence.PersonnelManagerDateAccept = DateTime.Now;

                        }
                        if (model.IsAbsenceTypeEditable)
                        {
// ReSharper disable PossibleInvalidOperationException
                            absence.BeginDate = model.BeginDate.Value;
                            absence.EndDate = model.EndDate.Value;
// ReSharper restore PossibleInvalidOperationException
                            absence.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                            absence.Type = AbsenceTypeDao.Load(model.AbsenceTypeId);
                        }
                        AbsenceDao.SaveAndFlush(absence);
                    }
                    if (absence.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = absence.Number.ToString();
                model.Version = absence.Version;
                model.DaysCount = absence.DaysCount;
                model.CreatorLogin = absence.Creator.Login;
                model.DateCreated = absence.CreateDate.ToShortDateString();
                SetFlagsState(absence.Id, user, absence, model);
                //throw new ArgumentException("Test exception");
                return true;
            }
            catch (Exception ex)
            {
                AbsenceDao.RollbackTran();
                Log.Error("Error on SaveAbsenceEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Absence);
                model.TimesheetStatuses = GetTimesheetStatusesForAbsence();
                model.AbsenceTypes = GetAbsenceTypes(false);
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.AbsenceTypeIdHidden = model.AbsenceTypeId;
                model.DaysCountHidden = model.DaysCount;
            }
        }
        #endregion
        #region Vacation list model
        public VacationListModel GetVacationListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            VacationListModel model = new VacationListModel
                                          {
                                              UserId = AuthenticationService.CurrentUser.Id,
                                              Department = new IdNameDto {Id=0,Name = string.Empty},
                                              VacationTypes = GetVacationTypes(true),
                                              RequestStatuses = GetRequestStatuses(),
                                              Positions = GetPositions(user)
                                          };
            return model;
        }
        public void SetVacationListModel(VacationListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            //model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
            model.VacationTypes = GetVacationTypes(true);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model,user);
        }
        public void SetDocumentsToModel(VacationListModel model,User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            /*Department dep = null;
            if(model.Department.Id != 0)
             dep = DepartmentDao.SearchByNameDistinct(model.Department.Name);*/
            model.Documents = VacationDao.GetDocuments(user.Id,
                role,
                GetDepartmentId(model.Department),
                model.PositionId,
                model.VacationTypeId,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate);
        }
        public List<IdNameDto> GetDepartments(User user)
        {
            //var departmentList = DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //departmentList.Insert(0,new IdNameDto(0,SelectAll));
            //var departmentList = UserToDepartmentDao.GetByUserId(user.Id).ToList();
            if((UserRole)user.Role.Id != UserRole.Employee)
            {
                var departmentList = DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                departmentList.Insert(0,new IdNameDto(0,SelectAll));
                return departmentList;
            }
            if(user.Department == null)
                return new List<IdNameDto> { new IdNameDto(0, SelectAll) };
            return new List<IdNameDto> { new IdNameDto(user.Department.Id, user.Department.Name) };
        }
        public List<IdNameDto> GetVacationTypes(bool addAll)
        {
            IList<VacationType> list =  VacationTypeDao.LoadAllSorted();
            List<IdNameDto> vacationTypeList = new List<IdNameDto>();
            vacationTypeList = list.
                    Where(x =>
                    x.Name == "Оплата дня сдачи крови и доп. дня отдыха донорам #1125" ||
                    x.Name == "Оплата дополнительного отпуска по календарным дням #1207" ||
                    x.Name == "Оплата дополнительных выходных дней по уходу за детьми - инвалидами #1504" ||
                    x.Name == "Оплата отпуска по календарным дням #1201" ||
                    x.Name == "Оплата учебного отпуска по календарным дням #1204" ||
                    x.Name == "Отпуск без оплаты согласно ТК РФ #1205"
                    )
                .ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //if( addAll 
            //    || AuthenticationService.CurrentUser.UserRole == UserRole.Manager
            //    || AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager)
            vacationTypeList.AddRange(list.Where(x => x.Name == "Отпуск по уходу за ребенком без оплаты #1802")
                    .ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name)));
            vacationTypeList = vacationTypeList.OrderBy(x => x.Name).ToList();
            if(addAll)
                vacationTypeList.Insert(0,new IdNameDto(0,SelectAll));
            return vacationTypeList;
        }
        public List<IdNameDto> GetRequestStatuses()
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> requestStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Новые"),
                                                           new IdNameDto(2, "Одобренные сотрудником"),
                                                           new IdNameDto(3, "Не одобренные сотрудником"),
                                                           new IdNameDto(4, "Одобренные руководителем"),
                                                           new IdNameDto(5, "Не одобренные руководителем"),
                                                           new IdNameDto(6, "Одобренные кадровиком"),
                                                           new IdNameDto(7, "Не одобренные кадровиком"),
                                                           new IdNameDto(8, "Одобренные всеми"),
                                                           new IdNameDto(9, "Выгруженные в 1С"),
                                                       };
            requestStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return requestStatusesList;
        }
        public List<IdNameDto> GetPositions(User user)
        {
            List<IdNameDto> positionList;
            if ((UserRole)user.Role.Id != UserRole.Employee)
            {
                positionList = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                positionList.Insert(0, new IdNameDto(0, SelectAll));
            }
            else
            {
                Position position = user.Position;
                positionList = position != null 
                    ? new List<IdNameDto> {new IdNameDto(position.Id,position.Name)} 
                    : new List<IdNameDto> { new IdNameDto(0, SelectAll) };
            }
            return positionList;
        }
        #endregion
        #region Vacation edit model
        public VacationEditModel GetVacationEditModel(int id,int userId)
        {
            VacationEditModel model = new VacationEditModel {Id = id, UserId = userId};
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id, RequestAttachmentTypeEnum.Vacation);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.Vacation);
            model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            model.VacationTypes = GetVacationTypes(false);
            Vacation vacation = null; 
            if(id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                vacation = VacationDao.Load(id);
                if(vacation == null)
                    throw new ArgumentException(string.Format("Заявка на отпуск (id {0}) не найдена в базе данных.",id));
                model.Version = vacation.Version;
                model.VacationTypeId = vacation.Type.Id;
                model.VacationTypeIdHidden = model.VacationTypeId;
                model.BeginDate = vacation.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = vacation.EndDate;
                model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                model.TimesheetStatusIdHidden = model.TimesheetStatusId; 
                model.DaysCount = vacation.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                model.CreatorLogin = vacation.Creator.Login;
                model.DocumentNumber = vacation.Number.ToString();
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                if (vacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user,vacation, model);
            return model;
        }
        public bool SaveVacationEditModel(VacationEditModel model, UploadFileDto fileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Vacation vacation;
                if(model.Id == 0)
                {
                    vacation = new Vacation
                                            {
// ReSharper disable PossibleInvalidOperationException
                                                BeginDate = model.BeginDate.Value,
                                                EndDate = model.EndDate.Value,
// ReSharper restore PossibleInvalidOperationException
                                                CreateDate = DateTime.Now,
                                                Creator = UserDao.Load(current.Id),
                                                DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1,
                                                Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Vacation),
                                                //Status = RequestStatusDao.Load((int) RequestStatusEnum.NotApproved),
                                                Type = VacationTypeDao.Load(model.VacationTypeId),
                                                User = user,
                                                //UserFullNameForPrint = user.FullName, 
                                             };
                    if (current.UserRole == UserRole.Employee && current.Id == model.UserId && model.IsApprovedByUser)
                        vacation.UserDateAccept = DateTime.Now;
                    if (current.UserRole == UserRole.Manager && user.Manager != null
                        && current.Id == user.Manager.Id)
                    {
                        vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if(model.IsApprovedByManager)
                            vacation.ManagerDateAccept = DateTime.Now;
                    }
                    if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                        && current.Id == user.PersonnelManager.Id )
                    {
                        vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApprovedByPersonnelManager)
                            vacation.PersonnelManagerDateAccept = DateTime.Now;
                    }

                    VacationDao.SaveAndFlush(vacation);
                    model.Id = vacation.Id;
                }
                else
                {
                    vacation = VacationDao.Load(model.Id);
                    string fileName;
                    int? attachmentId = SaveAttachment(vacation.Id, model.AttachmentId, fileDto, RequestAttachmentTypeEnum.Vacation, out fileName);
                    if (attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }
                    if (vacation.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                        vacation.DeleteDate = DateTime.Now;
                        VacationDao.SaveAndFlush(vacation);
                        model.IsDelete = false;
                        //model.VacationTypeId = vacation.Type.Id;
                        //model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                    }
                    else
                    {
                        if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                            && !vacation.UserDateAccept.HasValue 
                            && model.IsApprovedByUser)
                            vacation.UserDateAccept = DateTime.Now;
                        
                        if (current.UserRole == UserRole.Manager && user.Manager != null
                            && current.Id == user.Manager.Id
                            && !vacation.ManagerDateAccept.HasValue )
                        {
                            vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if(model.IsApprovedByManager)
                                vacation.ManagerDateAccept = DateTime.Now;
                        }
                        if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                            && current.Id == user.PersonnelManager.Id
                            && !vacation.PersonnelManagerDateAccept.HasValue)
                        {
                            vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (model.IsApprovedByPersonnelManager)
                                vacation.PersonnelManagerDateAccept = DateTime.Now;

                        }
                        if (model.IsVacationTypeEditable)
                        {
// ReSharper disable PossibleInvalidOperationException
                            vacation.BeginDate = model.BeginDate.Value;
                            vacation.EndDate = model.EndDate.Value;
// ReSharper restore PossibleInvalidOperationException
                            vacation.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1;
                            vacation.Type = VacationTypeDao.Load(model.VacationTypeId);
                        }
                        //vacation.UserFullNameForPrint = user.FullName;
                        VacationDao.SaveAndFlush(vacation);
                    }
                    if (vacation.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = vacation.Number.ToString();
                model.Version = vacation.Version;
                model.DaysCount = vacation.DaysCount;
                model.CreatorLogin = vacation.Creator.Login;
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                SetFlagsState(vacation.Id,user,vacation,model);
                return true;
            }
            catch (Exception ex)
            {
                AbsenceDao.RollbackTran();
                Log.Error("Error on GetVacationEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Vacation);
                model.TimesheetStatuses = GetTimesheetStatusesForVacation();
                model.VacationTypes = GetVacationTypes(false);
                model.VacationTypeIdHidden = model.VacationTypeId;
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.DaysCountHidden = model.DaysCount;
            }
        }
        public bool CheckUserRights(User user, IUser current,int entityId,bool isSave)
        {
            switch (current.UserRole)
            {
                case UserRole.Employee:
                    if (user.Id != current.Id)
                        return false;
                    break;
                case UserRole.Manager:
                    if (user.Manager != null && user.Manager.Id != current.Id)
                        return false;
                    break;
                case UserRole.PersonnelManager:
                    if (user.PersonnelManager != null && user.PersonnelManager.Id != current.Id)
                        return false;
                    break;
                case UserRole.Inspector:
                    if (entityId == 0)
                        throw new ArgumentException("Вам запрещено создавать новые заявки.");
                    if(isSave)
                        throw new ArgumentException("Вам запрещено редактировать заявки.");
                    return InspectorToUserDao.IsInspectorToUserRecordExists(current.Id, user.Id);
            }
            return true;
        }
        public void ReloadDictionariesToModel(VacationEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Vacation);
            model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            model.VacationTypes = GetVacationTypes(false);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Vacation vacation = VacationDao.Load(model.Id);
                model.CreatorLogin = vacation.Creator.Login;
                model.DocumentNumber = vacation.Number.ToString();
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                model.DaysCount = vacation.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                if (vacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
        }
        protected void SetFlagsState(VacationEditModel model,bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsVacationTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
            model.IsPrintAvailable = state;
        }
        protected void SetFlagsState(int id,User user,Vacation vacation,VacationEditModel model)
        {
            SetFlagsState(model,false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if(id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsVacationTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        break;
                }
                return;
            }
            
            model.IsApprovedByUserHidden = model.IsApprovedByUser = vacation.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = vacation.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden =
                model.IsApprovedByPersonnelManager = vacation.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = vacation.SendTo1C.HasValue;

            RequestPrintForm form = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.Vacation);
            model.IsPrintAvailable = form != null;

            switch(currentUserRole)
            {
                case UserRole.Employee:
                    if (!vacation.UserDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if(!vacation.ManagerDateAccept.HasValue && !vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                            model.IsVacationTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!vacation.ManagerDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                       if (!vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!vacation.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!vacation.SendTo1C.HasValue && 
                             !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    
                    break;
            }
            model.IsSaveAvailable = model.IsVacationTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetUserInfoModel(User user,UserInfoModel model)
        {
            //model.DateCreated = DateTime.Today.ToShortDateString();
            //IList<IdNameDto> departments = UserToDepartmentDao.GetByUserId(user.Id);
            //if (departments.Count > 0)
            model.Department = user.Department == null?string.Empty:user.Department.Name ;
            if(user.Manager != null)
                model.ManagerName = user.Manager.FullName;
            if (user.PersonnelManager != null)
                model.PersonnelName = user.PersonnelManager.FullName;
            if (user.Organization != null)
                model.Organization = user.Organization.Name;
            if(user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
            model.UserNumber = user.Code;
        }
        protected List<IdNameDto> GetTimesheetStatusesForVacation()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id >= VacationFirstTimesheetStatisId) && (x.Id <= VacationLastTimesheetStatisId)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if(AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0,new IdNameDto(0,string.Empty));
            return dtos;
        }
        #endregion
        #region Comments
        public  RequestCommentsModel GetCommentsModel(int id,int typeId)
        {
            return SetCommentsModel(id,typeId);
        }
        protected RequestCommentsModel SetCommentsModel(int id,int typeId)
        {
            RequestCommentsModel commentModel = new RequestCommentsModel 
            { 
                RequestId = id 
                ,RequestTypeId = typeId 
                ,Comments = new List<RequestCommentModel>() };
            if (id > 0)
            {
                switch(typeId)
                {
                    case (int)RequestTypeEnum.Vacation:
                        Vacation vacation = VacationDao.Load(id);
                        if ((vacation.Comments != null) && (vacation.Comments.Count() > 0))
                        {
                            commentModel.Comments = vacation.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                                    {
                                                        Comment = x.Comment,
                                                        CreatedDate = x.DateCreated.ToString(),
                                                        Creator = x.User.FullName,
                                                    });
                        }
                    break;
                    case (int)RequestTypeEnum.Absence:
                    Absence absence = AbsenceDao.Load(id);
                    if ((absence.Comments != null) && (absence.Comments.Count() > 0))
                    {
                        commentModel.Comments = absence.Comments.OrderBy(x => x.DateCreated).ToList().
                            ConvertAll(x => new RequestCommentModel
                            {
                                Comment = x.Comment,
                                CreatedDate = x.DateCreated.ToString(),
                                Creator = x.User.FullName,
                            });
                    }
                    break;
                    case (int)RequestTypeEnum.Sicklist:
                    Sicklist sicklist = SicklistDao.Load(id);
                    if ((sicklist.Comments != null) && (sicklist.Comments.Count() > 0))
                    {
                        commentModel.Comments = sicklist.Comments.OrderBy(x => x.DateCreated).ToList().
                            ConvertAll(x => new RequestCommentModel
                            {
                                Comment = x.Comment,
                                CreatedDate = x.DateCreated.ToString(),
                                Creator = x.User.FullName,
                            });
                    }
                    break;
                    case (int)RequestTypeEnum.HolidayWork:
                    HolidayWork holidayWork = HolidayWorkDao.Load(id);
                    if ((holidayWork.Comments != null) && (holidayWork.Comments.Count() > 0))
                    {
                        commentModel.Comments = holidayWork.Comments.OrderBy(x => x.DateCreated).ToList().
                            ConvertAll(x => new RequestCommentModel
                            {
                                Comment = x.Comment,
                                CreatedDate = x.DateCreated.ToString(),
                                Creator = x.User.FullName,
                            });
                    }
                    break;
                    case (int)RequestTypeEnum.Mission:
                    Mission mission = MissionDao.Load(id);
                    if ((mission.Comments != null) && (mission.Comments.Count() > 0))
                    {
                        commentModel.Comments = mission.Comments.OrderBy(x => x.DateCreated).ToList().
                            ConvertAll(x => new RequestCommentModel
                            {
                                Comment = x.Comment,
                                CreatedDate = x.DateCreated.ToString(),
                                Creator = x.User.FullName,
                            });
                    }
                    break;
                    case (int)RequestTypeEnum.Dismissal:
                    Dismissal dismissal = DismissalDao.Load(id);
                    if ((dismissal.Comments != null) && (dismissal.Comments.Count() > 0))
                    {
                        commentModel.Comments = dismissal.Comments.OrderBy(x => x.DateCreated).ToList().
                            ConvertAll(x => new RequestCommentModel
                            {
                                Comment = x.Comment,
                                CreatedDate = x.DateCreated.ToString(),
                                Creator = x.User.FullName,
                            });
                    }
                    break;
                    case (int)RequestTypeEnum.TimesheetCorrection:
                    TimesheetCorrection timesheetCorrection = TimesheetCorrectionDao.Load(id);
                    if ((timesheetCorrection.Comments != null) && (timesheetCorrection.Comments.Count() > 0))
                    {
                        commentModel.Comments = timesheetCorrection.Comments.OrderBy(x => x.DateCreated).ToList().
                            ConvertAll(x => new RequestCommentModel
                            {
                                Comment = x.Comment,
                                CreatedDate = x.DateCreated.ToString(),
                                Creator = x.User.FullName,
                            });
                    }
                    break;
                    case (int)RequestTypeEnum.Employment:
                    Employment employment = EmploymentDao.Load(id);
                    if ((employment.Comments != null) && (employment.Comments.Count() > 0))
                    {
                        commentModel.Comments = employment.Comments.OrderBy(x => x.DateCreated).ToList().
                            ConvertAll(x => new RequestCommentModel
                            {
                                Comment = x.Comment,
                                CreatedDate = x.DateCreated.ToString(),
                                Creator = x.User.FullName,
                            });
                    }
                    break;
                }
            }
            return commentModel;
        }
        public bool SaveComment(SaveCommentModel model)
        {
            try
            {
                int userId = AuthenticationService.CurrentUser.Id;
                User user;
                switch(model.TypeId)
                {
                    case (int)RequestTypeEnum.Vacation:
                        Vacation vacation = VacationDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        VacationComment comment = new VacationComment
                                                      {
                                                          Comment = model.Comment,
                                                          Vacation = vacation,
                                                          DateCreated = DateTime.Now,
                                                          User = user,
                                                      };
                        VacationCommentDao.MergeAndFlush(comment);
                        break;
                    case (int)RequestTypeEnum.Absence:
                        Absence absence = AbsenceDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        AbsenceComment absenceComment = new AbsenceComment
                        {
                            Comment = model.Comment,
                            Absence = absence,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        AbsenceCommentDao.MergeAndFlush(absenceComment);
                        break;
                    case (int)RequestTypeEnum.Sicklist:
                        Sicklist sicklist = SicklistDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        SicklistComment sicklistComment = new SicklistComment
                        {
                            Comment = model.Comment,
                            Sicklist = sicklist,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        SicklistCommentDao.MergeAndFlush(sicklistComment);
                        break;
                    case (int)RequestTypeEnum.HolidayWork:
                        HolidayWork holidayWork = HolidayWorkDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        HolidayWorkComment holidayWorkComment = new HolidayWorkComment
                        {
                            Comment = model.Comment,
                            HolidayWork = holidayWork,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        HolidayWorkCommentDao.MergeAndFlush(holidayWorkComment);
                        break;
                    case (int)RequestTypeEnum.Mission:
                        Mission mission = MissionDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        MissionComment missionComment = new MissionComment
                        {
                            Comment = model.Comment,
                            Mission = mission,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        MissionCommentDao.MergeAndFlush(missionComment);
                        break;
                    case (int)RequestTypeEnum.Dismissal:
                        Dismissal  dismissal = DismissalDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        DismissalComment dismissalComment = new DismissalComment
                        {
                            Comment = model.Comment,
                            Dismissal = dismissal,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        DismissalCommentDao.MergeAndFlush(dismissalComment);
                        break;
                    case (int)RequestTypeEnum.TimesheetCorrection:
                        TimesheetCorrection timesheetCorrection = TimesheetCorrectionDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        TimesheetCorrectionComment timesheetCorrectionComment = new TimesheetCorrectionComment
                        {
                            Comment = model.Comment,
                            TimesheetCorrection = timesheetCorrection,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        TimesheetCorrectionCommentDao.MergeAndFlush(timesheetCorrectionComment);
                        break;
                    case (int)RequestTypeEnum.Employment:
                        Employment employment = EmploymentDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        EmploymentComment employmentComment = new EmploymentComment
                        {
                            Comment = model.Comment,
                            Employment = employment,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        EmploymentCommentDao.MergeAndFlush(employmentComment);
                        SendEmailForUserRequest(employment.User, AuthenticationService.CurrentUser, employment.Id,
                                                employment.Number, RequestTypeEnum.Employment, true);
                        break;
                }
                //doc.Comments.Add(comment);
                //DocumentDao.MergeAndFlush(doc);
                return true;
            }
            catch (Exception ex)
            {
                VacationDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }
        #endregion
        #region Attachment
        public RequestAttachmentsModel GetAttachmentsModel(int id, RequestAttachmentTypeEnum typeId)
        {
            bool isAddAvailable = false;
            bool isDeleteAvailable = false;
            List<RequestAttachment> list = RequestAttachmentDao.FindManyByRequestIdAndTypeId(id, typeId).ToList();
            if(id > 0)
            {
                Employment entity = EmploymentDao.Load(id);
                isAddAvailable = !entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue;
                isDeleteAvailable = !entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue;
                if(isDeleteAvailable && list.Count <= 4 )
                {
                     if((entity.UserDateAccept.HasValue && CurrentUser.UserRole == UserRole.Employee) ||
                        (entity.ManagerDateAccept.HasValue && CurrentUser.UserRole == UserRole.Manager) ||
                        (entity.PersonnelManagerDateAccept.HasValue && CurrentUser.UserRole == UserRole.PersonnelManager)
                       )
                         isDeleteAvailable = false;
                }
            }    
            RequestAttachmentsModel model = new RequestAttachmentsModel
            {
                AttachmentRequestId = id,
                AttachmentRequestTypeId =(int) typeId,
                IsAddAvailable = isAddAvailable,
                Attachments = new List<RequestAttachmentModel>()
            };
            model.Attachments =list. ConvertAll(x =>
                            new RequestAttachmentModel
                            {
                                Attachment = x.FileName, 
                                AttachmentId = x.Id, 
                                Description = x.Description,
                                IsDeleteAvailable = (x.CreatorUserRole == CurrentUser.UserRole) && isDeleteAvailable,
                            });
            return model;
        }

        public AttachmentModel GetFileContext(int id/*,int typeId*/)
        {
            RequestAttachment attachment = RequestAttachmentDao.Load(id);
            return new AttachmentModel
            {
                Context = attachment.UncompressContext,
                FileName = attachment.FileName,
                ContextType = attachment.ContextType
            };
        }
        public bool SaveAttachment(SaveAttacmentModel model)
        {
            RequestAttachment attach = new RequestAttachment
                                           {
                                               ContextType = GetFileContext(model.FileDto.FileName),
                                               DateCreated = DateTime.Now,
                                               Description = model.Description,
                                               FileName = model.FileDto.FileName,
                                               RequestId = model.EntityId,
                                               RequestType = (int)model.EntityTypeId,
                                               UncompressContext = model.FileDto.Context,
                                               CreatorRole = RoleDao.Load((int)CurrentUser.UserRole)

                                           };
            RequestAttachmentDao.SaveAndFlush(attach);
            return true;
        }
        public bool DeleteAttachment(DeleteAttacmentModel model)
        {
            RequestAttachmentDao.Delete(model.Id);
            return true;
        }
        public int GetAttachmentsCount(int entityId,RequestAttachmentTypeEnum typeId)
        {
            return RequestAttachmentDao.FindManyByRequestIdAndTypeId(entityId, typeId).Count;
        }
        public string GetFileContext(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            //string contextType;
            switch (extension)
            {
                case ".doc":
                case ".docx":
                    return  "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/ms-excel";
                default:
                    return "application/octet-stream";
            }
        }
        #endregion
        public AttachmentModel GetPrintFormFileContext(int id,RequestPrintFormTypeEnum typeId)
        {
            RequestPrintForm printForm = RequestPrintFormDao.FindByRequestAndTypeId(id,typeId);
            if(printForm == null)
                throw new ArgumentException(string.Format("Печатная форма для заявки (Id {0}) не найдена в базе данных",id));
            return new AttachmentModel
            {
                Context = printForm.Context,
                FileName = "PrintFrom",
                ContextType = "application/msword"
            };
        }
        
        public VacationPrintModel GetVacationPrintModel(int id)
        {
            Vacation vacation = VacationDao.Load(id);

            return new VacationPrintModel
            {
                AllBeginDateDay = vacation.BeginDate.Day.ToString(),
                AllBeginDateMonth = GetMonthName(vacation.BeginDate.Month),
                AllBeginDateYear = vacation.BeginDate.Year.ToString(),
                AllDaysNumber = vacation.DaysCount.ToString(),
                AllEndDateDay = vacation.EndDate.Day.ToString(),
                AllEndDateMonth = GetMonthName(vacation.EndDate.Month),
                AllEndDateYear = vacation.EndDate.Year.ToString(),
                BeginDateDay = vacation.BeginDate.Day.ToString(),
                BeginDateMonth = GetMonthName(vacation.BeginDate.Month),
                BeginDateYear = vacation.BeginDate.Year.ToString(),
                CreateDate = vacation.CreateDate.ToShortDateString(),
                DaysNumber = vacation.DaysCount.ToString(),
                Department = vacation.User.Department == null?string.Empty:vacation.User.Department.Name,
                DocNumber = vacation.Number.ToString(),
                EndDateDay = vacation.EndDate.Day.ToString(),
                EndDateMonth = GetMonthName(vacation.EndDate.Month),
                EndDateYear = vacation.EndDate.Year.ToString(),
                FIO = vacation.User.FullName,
                //ManagerFIO = 
                //ManagerPosition = 
                OrgName = vacation.User.Organization.Name,
                //PeriodBeginDateDay = vacation.BeginDate.Day.ToString(),
                //PeriodBeginDateMonth = GetMonthName(vacation.BeginDate.Month),
                //PeriodBeginDateYear = vacation.BeginDate.Year.ToString(),
                //PeriodEndDateDay = vacation.EndDate.Day.ToString(),
                //PeriodEndDateMonth = GetMonthName(vacation.EndDate.Month),
                //PeriodEndDateYear = vacation.EndDate.Year.ToString(),
                Position = vacation.User.Position.Name,
                TabNumber = vacation.User.Code,
            };
        }
        public void CreateVacationOrder(int id,string templatePath,string filePath)
        {
            _Application word = null;
            _Document sdoc = null;
            _Document ddoc = null;
            object noSave = WdSaveOptions.wdDoNotSaveChanges;
            object oMissing = Missing.Value;
            try
            {

                Vacation vacation = VacationDao.Load(id);
                List<PrintVacationOrderDto> list = GetdDtoList(vacation);
                
                //разделитель страниц http://msdn.microsoft.com/en-us/library/bb213704%28office.12%29.aspx
                //object pageBreak = WdBreakType.wdPageBreak;
                //не сохранять изменения
                word = new Application {Visible = false};
                //word.Options.DefaultFilePath[WdDefaultFilePath.wdUserTemplatesPath] = folderPath;
                //Можем сделать его видимым и смотреть как скачут слова, абзацы и страницы
                object objTemplatePath = templatePath;
                //Создаем временный документ, в котором будем заменять ключевые слова на наши
                sdoc = word.Documents.Add(ref objTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                for (int i = 0; i < sdoc.Words.Count; i++)
                {
                    //Log.DebugFormat("Text {0} at position {1} ", sdoc.Words[i + 1].Text, i + 1);
                    foreach (PrintVacationOrderDto dto in list)
                    {
                        if (sdoc.Words[i + 1].Text.Trim() == dto.Keyword) //не забываем, что ворд считает с единицы, а не нуля
                        {
                            Log.DebugFormat("Found keyword {0} (word number {1}) ",dto.Keyword, i+1);
                            dto.Position = i + 1;
                            dto.spacesAfter = sdoc.Words[i + 1].Text.Remove(0, dto.Keyword.Length);
                            //keyWordEntries.Add(new keyWordEntry(keyWord, i + 1, sdoc.Words[i + 1].Text.Remove(0, keyWord.Length)));
                        }
                    }
                }
                /*ddoc = word.Documents.Add(ref objTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).Delete(ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).InsertParagraphAfter();*/
                int positionCorrection = 0;
                foreach (PrintVacationOrderDto dto in list)
                {
                    /*if (dto.Keyword == "FIO")
                        positionCorrection = positionCorrection+2;*/
                    Log.DebugFormat("Setting {0} {1} {3} '{2}' to document", dto.Keyword, dto.Text, dto.spacesAfter,dto.Position+positionCorrection);
                    sdoc.Words[dto.Position + positionCorrection].Text = dto.Text + dto.spacesAfter;
                    string[] words = dto.Text.Split(new [] {' '});
                    positionCorrection += words.Count() - 1;
                    string[] wordsPoint = dto.Text.Split(new[] { '.'});
                    positionCorrection += 2*(wordsPoint.Count() - 1);
                }
                /*for (int i = 0; i < sdoc.Words.Count; i++)
                    Log.DebugFormat("Text {0} at position {1} ", sdoc.Words[i + 1].Text, i + 1);*/
                ddoc = word.Documents.Add(ref objTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).Delete(ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).InsertParagraphAfter();
                sdoc.Range(ref oMissing, ref oMissing).Copy();
                ddoc.Paragraphs[1].Range.Paste();
                sdoc.Close(ref noSave, ref oMissing, ref oMissing);
                sdoc = null;
                object objFilePath = filePath;
                //сахраняем полученный документ
                ddoc.SaveAs(ref objFilePath, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                //закрываем полученный документ
                ddoc.Close(ref oMissing, ref oMissing, ref oMissing);
                ddoc = null;
                //завершаем наш процесс ворда
                word.Quit(ref oMissing, ref oMissing, ref oMissing);
                word = null;
            }
            catch (Exception ex)
            {
                Log.Error("Exception on CreateVacationOrder:",ex);
                throw;
            }
            finally
            {
                if(sdoc != null)
                    sdoc.Close(ref noSave,ref oMissing, ref oMissing);
                if(ddoc != null)
                    ddoc.Close(ref oMissing, ref oMissing, ref oMissing);
                if(word != null)
                    word.Quit(ref oMissing, ref oMissing, ref oMissing);
            }
            
        }
        protected List<PrintVacationOrderDto> GetdDtoList(Vacation vacation)
        {
            return new List<PrintVacationOrderDto>
                       {
                           new PrintVacationOrderDto { Keyword = "ORGNAME",Text = vacation.User.Organization.Name},
                           new PrintVacationOrderDto { Keyword = "DOCNUM",Text = vacation.Number.ToString()},
                           new PrintVacationOrderDto { Keyword = "DOCDATE",Text = vacation.CreateDate.ToShortDateString()},
                           new PrintVacationOrderDto { Keyword = "FIO",Text = vacation.User.FullName},
                           new PrintVacationOrderDto { Keyword = "TABNUM",Text = vacation.User.Code},
                           new PrintVacationOrderDto { Keyword = "DEPARTMENT",Text = "Тест"},
                           new PrintVacationOrderDto { Keyword = "POSITION",Text = vacation.User.Position.Name},
                           new PrintVacationOrderDto { Keyword = "VT",Text = vacation.DaysCount.ToString()},
                           new PrintVacationOrderDto { Keyword = "BDD",Text = vacation.BeginDate.Day.ToString()},
                           new PrintVacationOrderDto { Keyword = "BM",Text = GetMonthName(vacation.BeginDate.Month)},
                           new PrintVacationOrderDto { Keyword = "Y",Text = vacation.BeginDate.Year.ToString().Substring(2)},
                           new PrintVacationOrderDto { Keyword = "ED",Text = vacation.EndDate.Day.ToString()},
                           new PrintVacationOrderDto { Keyword = "EDM",Text = GetMonthName(vacation.BeginDate.Month)},
                           new PrintVacationOrderDto { Keyword = "EY",Text = vacation.EndDate.Year.ToString().Substring(2)},
                           new PrintVacationOrderDto { Keyword = "MANPOS",Text = vacation.User.Manager.Position.Name},
                           new PrintVacationOrderDto { Keyword = "MANFIO",Text = vacation.User.Manager.Name},
                       };
        }
        protected static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "cентября";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                case 12:
                    return "декабря";
                default:
                    throw new ArgumentException(string.Format("Неизвестный месяц {0}", month));
            }
        }

    }

}