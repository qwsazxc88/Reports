using System;
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
            if (id != 0)
            {
                entity = AppointmentDao.Load(id);
                if (entity == null)
                    throw new ValidationException(string.Format(StrAppointmentNotFound, id));
            }
            //User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            //if (!CheckUserRights(current, id, entity, false))
            //    throw new ArgumentException(StrAccessIsDenied);

            //model.Id = entity.Id;
            //model.Version = entity.Version;
            //model.DocumentTitle = string.Format("Авансовый отчет № АО{0} о командировке к Приказу № {0} на командировку", entity.Number);
            //model.DocumentNumber = entity.Number.ToString();
            //model.DateCreated = entity.CreateDate.ToShortDateString();
            //model.Hotels = entity.Hotels;

            //SetUserInfoModel(user, model);
            //LoadDictionaries(model);
            //SetFlagsState(id, user, entity, model);
            //SetHiddenFields(model);
            return model;
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