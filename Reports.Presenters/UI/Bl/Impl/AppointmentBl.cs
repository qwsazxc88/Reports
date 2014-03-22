﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class AppointmentBl : BaseBl, IAppointmentBl
    {
        public const string StrException = "Исключение:";
        public const string StrCommentCreationDedied = "Добавление комментария запрещено";
        public const string StrAppointmentNotFound = "Не найдена заявка (id {0}) в базе данных";
        public const string StrAccessIsDenied = "Доступ запрещен";
        public const string StrUserNotManager = "Вы (пользователь {0}) не являетесь руководителем или членом правления - создание заявки запрещено";
        public const string StrIncorrectManagerLevel  = "Неправильный уровень {0} руководителя (id {1}) в базе данных.";
        public const string StrNoDepartmentForManager = "Не указано структурное подраздаление для руководителя (id {0}).";
        public const string StrIncorrectReasonId = "Неверное основание появления вакансии {0}.";
        #region DAOs
        protected IAppointmentDao appointmentDao;
        public IAppointmentDao AppointmentDao
        {
            get { return Validate.Dependency(appointmentDao); }
            set { appointmentDao = value; }
        }
        protected IAppointmentCommentDao appointmentCommentDao;
        public IAppointmentCommentDao AppointmentCommentDao
        {
            get { return Validate.Dependency(appointmentCommentDao); }
            set { appointmentCommentDao = value; }
        }
        protected IPositionDao positionDao;
        public IPositionDao PositionDao
        {
            get { return Validate.Dependency(positionDao); }
            set { positionDao = value; }
        }
        protected IAppointmentReasonDao appointmentReasonDao;
        public IAppointmentReasonDao AppointmentReasonDao
        {
            get { return Validate.Dependency(appointmentReasonDao); }
            set { appointmentReasonDao = value; }
        }
        #endregion

        public AppointmentListModel GetAppointmentListModel()
        {
            //User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //IdNameReadonlyDto dep = GetDepartmentDto(user);
            AppointmentListModel model = new AppointmentListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = string.Empty,
                DepartmentId = 0,
                DepartmentReadOnly = false,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            //SetInitialStatus(model);
            //SetIsAvailable(model);
            return model;
        }
        public void SetDictionariesToModel(AppointmentListModel model)
        {
            model.Statuses = GetAppRequestStatuses();
        }
        protected List<IdNameDto> GetAppRequestStatuses()
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Заявка создана"),
                                                           new IdNameDto(2, "Не одобрена вышестоящим руководителем"),
                                                           new IdNameDto(3, "Одобрена вышестоящим руководителем"),
                                                           new IdNameDto(4, "Принята в работу"),
                                                           new IdNameDto(5, "Отменена"),
                                                           //new IdNameDto(4, "Не одобрен руководителем"),
                                                           //new IdNameDto(6, "Не одобрен бухгалтером"),
                                                           //new IdNameDto(7, "Требует одобрения руководителем"),
                                                           //new IdNameDto(8, "Требует одобрения бухгалтером"),
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }

        public AppointmentEditModel GetAppointmentEditModel(int id)
        {
            AppointmentEditModel model = new AppointmentEditModel {Id = id};
            Appointment entity = null;
            User creator;
            IUser current = AuthenticationService.CurrentUser;
            User currUser = UserDao.Load(current.Id);
            if (id != 0)
            {
                entity = AppointmentDao.Load(id);
                if (entity == null)
                    throw new ValidationException(string.Format(StrAppointmentNotFound, id));
                creator = entity.Creator;
                model.AdditionalRequirements = entity.AdditionalRequirements;
                model.Bonus = FormatSum(entity.Bonus);
                model.City = entity.City;
                model.Compensation = entity.Compensation;
                model.DateCreated = FormatDate(entity.CreateDate);
                model.DepartmentId = entity.Department.Id;
                model.DepartmentName = entity.Department.Name;
                model.DesirableBeginDate = FormatDate(entity.DesirableBeginDate);
                model.EducationRequirements = entity.EducationRequirements;
                model.ExperienceRequirements = entity.ExperienceRequirements;
                model.IsVacationExists = entity.IsVacationExists ? 1 : 0;
                model.DocumentNumber = entity.Number.ToString();
                model.OtherRequirements = entity.OtherRequirements;
                model.Period = entity.Period;
                model.PositionId = entity.Position.Id;
                model.ReasonId = entity.Reason.Id;
                // hardcode using database Ids from [dbo].[AppointmentReason] 
                switch (entity.Reason.Id)
                {
                    case 1:
                    case 2:
                        model.ReasonPosition = entity.ReasonPosition;
                        model.ReasonBeginDate = FormatDate(entity.ReasonBeginDate);
                        break;
                    case 3:
                        model.ReasonPosition = entity.ReasonPersonnelStore;
                        break;
                    case 4:
                    case 5:
                        model.ReasonPosition = entity.ReasonUser;
                        model.ReasonBeginDate = FormatDate(entity.ReasonBeginDate);
                        break;
                    default:
                        throw new ArgumentException(string.Format(StrIncorrectReasonId,entity.Reason.Id));
                }
                model.Responsibility = entity.Responsibility;
                model.Salary = FormatSum(entity.Salary);
                model.Schedule = entity.Schedule;
                model.TypeId = entity.Type?1:0;
                model.UserId = entity.Creator.Id;//todo ???
                model.VacationCount = entity.VacationCount.ToString();
                model.Version = entity.Version;
            }
            else
            {
                creator = currUser;
            }
            //if (!CheckUserRights(current, id, entity, false)) todo ???
            //    throw new ArgumentException(StrAccessIsDenied);
            SetManagerInfoModel(creator, model);
            LoadDictionaries(model);
            SetFlagsState(id, currUser, entity, model);
            SetHiddenFields(model);
            return model;
        }
        protected void SetFlagsState(int id, User current, Appointment entity, AppointmentEditModel model)
        {
            SetFlagsState(model, false);
            if(model.Id == 0)
            {
                if (current.UserRole != UserRole.Manager && current.UserRole != UserRole.Director)
                    throw new ArgumentException(string.Format(StrUserNotManager, current.Id));
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                model.IsManagerApproveAvailable = true;
                return;
            }
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue;
            model.IsChiefApproved = entity.ChiefDateAccept.HasValue;
            model.IsPersonnelApproved = entity.PersonnelDateAccept.HasValue;
            model.IsStaffApproved = entity.StaffDateAccept.HasValue;
            model.IsDeleted = entity.DeleteDate.HasValue;
            if (entity.AcceptManager != null && entity.ManagerDateAccept.HasValue)
                model.ManagerFio = entity.AcceptManager.FullName;
            if (entity.AcceptChief != null && entity.ChiefDateAccept.HasValue)
                model.ChiefFio = entity.AcceptChief.FullName;
            if (entity.AcceptPersonnel != null && entity.PersonnelDateAccept.HasValue)
                model.PersonnelFio = entity.AcceptPersonnel.FullName;
            if (entity.AcceptStaff != null && entity.StaffDateAccept.HasValue)
                model.StaffFio = entity.AcceptStaff.FullName;

            if(entity.DeleteDate.HasValue)
            {
                if (entity.DeleteUser.Id == entity.Creator.Id)
                    model.ManagerFio = entity.DeleteUser.FullName;
                else
                    model.ChiefFio = entity.DeleteUser.FullName;
                model.IsStaffReceiveRejectMail = true;// todo Need flag on entity ?
                if (entity.AcceptStaff != null)
                    model.StaffFio = entity.AcceptStaff.FullName;
            }
            switch (current.UserRole)
            {
                case UserRole.Manager:
                    if(current.Id == entity.Creator.Id && !entity.DeleteDate.HasValue)
                    {
                            model.IsManagerRejectAvailable = true;
                            if (!entity.ManagerDateAccept.HasValue)
                            {
                                model.IsManagerApproveAvailable = true;
                                model.IsEditable = true;
                            }
                    }
                    else if (!entity.DeleteDate.HasValue && IsManagerChiefForCreator(current, entity.Creator))
                    {
                        if(entity.ManagerDateAccept.HasValue)
                        {
                            model.IsManagerRejectAvailable = true;
                            if (!entity.ChiefDateAccept.HasValue)
                                model.IsChiefApproveAvailable = true;
                        }
                    }
                    break;
                case UserRole.Director:
                    if (!entity.DeleteDate.HasValue)
                    {
                        model.IsManagerRejectAvailable = true;
                        if (!entity.ChiefDateAccept.HasValue)
                            model.IsChiefApproveAvailable = true;
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.DeleteDate.HasValue && entity.ChiefDateAccept.HasValue &&
                        !entity.PersonnelDateAccept.HasValue)
                        model.IsPersonnelApproveAvailable = true;
                    
                    break;
                case UserRole.StaffManager:
                    if (!entity.DeleteDate.HasValue && entity.ChiefDateAccept.HasValue &&
                         entity.PersonnelDateAccept.HasValue && !entity.StaffDateAccept.HasValue)
                        model.IsStaffApproveAvailable = true;
                    break;
                case UserRole.OutsourcingManager:
                    break;
            }
            model.IsSaveAvailable = model.IsEditable || model.IsManagerApproveAvailable 
                || model.IsChiefApproveAvailable || model.IsManagerRejectAvailable ||
                model.IsPersonnelApproveAvailable || model.IsStaffApproveAvailable;
        }
        protected bool IsManagerChiefForCreator(User current, User creator)
        {
            if(current.Level < 2 || current.Level > 6)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,current.Level,current.Id));
            if(creator.Department == null)
                throw new ValidationException(string.Format(StrNoDepartmentForManager, creator.Id));
            if (current.Department == null)
                throw new ValidationException(string.Format(StrNoDepartmentForManager, current.Id));
            switch (current.Level)
            {
                default:
                    return current.Level < creator.Level &&
                           creator.Department.Path.StartsWith(current.Department.Path);
            }
        }
        protected void SetFlagsState(AppointmentEditModel model, bool state)
        {
            model.IsEditable = state;
            model.IsSaveAvailable = state;
            model.IsChiefApproveAvailable = state;
            model.IsManagerApproveAvailable = state;
            model.IsManagerRejectAvailable = state;
            model.IsPersonnelApproveAvailable = state;
            model.IsStaffApproveAvailable = state;
        }
        protected void LoadDictionaries(AppointmentEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id,RequestTypeEnum.Appointment);
            model.Types = new List<IdNameDto>
                              {
                                  new IdNameDto {Id = 0,Name = "Бессрочная"},
                                  new IdNameDto {Id = 1,Name = "Срочная"},
                              };
            model.Positions = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name});
            model.Reasons = AppointmentReasonDao.LoadAll().ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            model.IsVacationExistsValues = new List<IdNameDto>
                              {
                                  new IdNameDto {Id = 1,Name = "Есть"},
                                  new IdNameDto {Id = 0,Name = "Нет"},
                              };
        }
        protected void SetHiddenFields(AppointmentEditModel model)
        {
            model.IsVacationExistsHidden = model.IsVacationExists;
            model.PositionIdHidden = model.PositionId;
            model.ReasonIdHidden = model.ReasonId;
            model.TypeIdHidden = model.TypeId;
            model.IsManagerApprovedHidden = model.IsManagerApproved;
            model.IsChiefApprovedHidden = model.IsChiefApproved;
            model.IsPersonnelApprovedHidden = model.IsPersonnelApproved;
            model.IsStaffApprovedHidden = model.IsStaffApproved;
        }
        protected void SetManagerInfoModel(User user, ManagerInfoModel model)
        {
            model.Department = user.Department == null ? string.Empty : user.Department.Name;
            if (user.Organization != null)
                model.Organization = user.Organization.Name;
            if (user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
        }
        #region Comments
        public CommentsModel GetCommentsModel(int id, RequestTypeEnum typeId)
        {
            CommentsModel commentModel = new CommentsModel
            {
                RequestId = id,
                RequestTypeId = (int)typeId,
                Comments = new List<RequestCommentModel>(),
                IsAddAvailable = AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && id > 0,
            };
            if (id == 0)
                return commentModel;
            Appointment entity = AppointmentDao.Load(id);
            if ((entity.Comments != null) && (entity.Comments.Count() > 0))
            {
                commentModel.Comments = entity.Comments.OrderBy(x => x.DateCreated).ToList().
                    ConvertAll(x => new RequestCommentModel
                    {
                        Comment = x.Comment,
                        CreatedDate = x.DateCreated.ToString(),
                        Creator = x.User.FullName,
                    });
            }
            return commentModel;
            //Vacation vacation = VacationDao.Load(id);
        }
        public bool SaveComment(SaveCommentModel model)
        {
            try
            {
                if (AuthenticationService.CurrentUser.UserRole != UserRole.PersonnelManager)
                {
                    model.Error = StrCommentCreationDedied;
                    return false;
                }
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                Appointment entity = AppointmentDao.Load(model.DocumentId);
                AppointmentComment comment = new AppointmentComment
                {
                    Comment = model.Comment,
                    Appointment = entity,
                    DateCreated = DateTime.Now,
                    User = user,
                };
                AppointmentCommentDao.MergeAndFlush(comment);
                return true;
            }
            catch (Exception ex)
            {
                AppointmentCommentDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        #endregion
    }
}