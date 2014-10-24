using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class HelpBl : BaseBl, IHelpBl
    {

        public const string StrCannotEditVersion = "Вам запрещено редактирование информации о версии";
        public const string StrCannotDeleteVersion = "Вам запрещено удаление информации о версии";
        public const string StrException = "Исключение:";

        public const string StrCannotEditFaq = "Вам запрещено редактирование информации";
        public const string StrCannotDeleteFaq = "Вам запрещено удаление информации";
        public const string StrNoUser = "Не указан сотрудник для заявки на услугу";
        #region DAOs
        protected IHelpVersionDao helpVersionDao;
        public IHelpVersionDao HelpVersionDao
        {
            get { return Validate.Dependency(helpVersionDao); }
            set { helpVersionDao = value; }
        }
        protected IHelpFaqDao helpFaqDao;
        public IHelpFaqDao HelpFaqDao
        {
            get { return Validate.Dependency(helpFaqDao); }
            set { helpFaqDao = value; }
        }
        protected IRequestAttachmentDao requestAttachmentDao;
        public IRequestAttachmentDao RequestAttachmentDao
        {
            get { return Validate.Dependency(requestAttachmentDao); }
            set { requestAttachmentDao = value; }
        }
        protected IRoleDao roleDao;
        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }
        #endregion

        #region Service Requests List
        public HelpServiceRequestsListModel GetServiceRequestsList()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            HelpServiceRequestsListModel model = new HelpServiceRequestsListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            //SetInitialStatus(model);
            SetIsAvailable(model);
            return model;
        }
        protected void SetIsAvailable(HelpServiceRequestsListModel model)
        {
            model.IsAddAvailable = model.IsAddAvailable = (CurrentUser.UserRole == UserRole.Manager);
            //|| (CurrentUser.UserRole == UserRole.Employee);
        }
        public void SetDictionariesToModel(HelpServiceRequestsListModel model)
        {
            model.Statuses = GetServiceRequestsStatuses();
        }
        public List<IdNameDto> GetServiceRequestsStatuses()
        {
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Услуга запрошена"),
                                                           new IdNameDto(2, "Услуга формируется"),
                                                           new IdNameDto(3, "Услуга оказана"),
                                                           //new IdNameDto(4, "Не одобрен руководителем"),
                                                           //new IdNameDto(5, "Одобрен членом правления"),
                                                           //new IdNameDto(6, "Не одобрен членом правления"),
                                                           //new IdNameDto(7, "Требует одобрения руководителем"),
                                                           //new IdNameDto(8, "Требует одобрения членом правления"),
                                                           //new IdNameDto(9, "Выгружен в 1С"),
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }

        public void SetServiceRequestsListModel(HelpServiceRequestsListModel model, bool hasError)
        {
            SetDictionariesToModel(model);
            User user = UserDao.Load(model.UserId);
            if (hasError)
                model.Documents = new List<HelpServiceRequestDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(HelpServiceRequestsListModel model, User user)
        {
            UserRole role = CurrentUser.UserRole;
            model.Documents = new List<HelpServiceRequestDto>();
            //model.Documents = MissionReportDao.GetDocuments(
            //    user.Id,
            //    role,
            //    //GetDepartmentId(model.Department),
            //    model.DepartmentId,
            //    //model.PositionId,
            //    //model.TypeId,
            //    //0,
            //    model.StatusId,
            //    model.BeginDate,
            //    model.EndDate,
            //    model.UserName,
            //    model.Number,
            //    model.SortBy,
            //    model.SortDescending);
        }
        #endregion
        #region Service Requests Edit
        public HelpServiceRequestEditModel GetServiceRequestEditModel(int id, int? userId)
        {
            if (id == 0 && !userId.HasValue)
            {
                if (CurrentUser.UserRole == UserRole.Employee)
                    userId = CurrentUser.Id;
                else
                    throw new ValidationException(StrNoUser);
            }
            HelpServiceRequestEditModel model = new HelpServiceRequestEditModel
            {
                Id = id,
                UserId = id == 0 ? userId.Value : 0 //entity.User.Id
            };
            User user = UserDao.Load(model.UserId);
            //MissionOrder entity = null;
            //if (id != 0)
            //{
            //    entity = MissionOrderDao.Load(id);
            //    if (entity == null)
            //        throw new ValidationException(string.Format("Не найден приказ на командировку (id {0}) в базе данных", id));
            //    if (entity.IsAdditional)
            //        throw new ValidationException("Редактирование изменения приказа на командировку невозможно через форму приказа.");
            //}
            //MissionOrderEditModel model = new MissionOrderEditModel
            //{
            //    Id = id,
            //    UserId = id == 0 ? userId.Value : entity.User.Id
            //};
            //User user = UserDao.Load(model.UserId);
            //if (!user.Grade.HasValue)
            //    throw new ValidationException(string.Format("Не указан грейд для пользователя {0} в базе данных", user.Id));
            //IUser current = AuthenticationService.CurrentUser;

            //if (!CheckUserMoRights(user, current, id, entity, false))
            //    throw new ArgumentException("Доступ запрещен.");
            ////model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.MissionOrder);
            //if (id != 0)
            //{

            //    LoadGraids(model, user.Grade.Value, entity, entity.CreateDate);
            //    model.AllSum = FormatSum(entity.AllSum);
            //    model.AllSumAir = FormatSum(entity.SumAir);
            //    model.AllSumDaily = FormatSum(entity.SumDaily);
            //    model.AllSumResidence = FormatSum(entity.SumResidence);
            //    model.AllSumTrain = FormatSum(entity.SumTrain);
            //    model.BeginMissionDate = FormatDate(entity.BeginDate);//entity.BeginDate.ToShortDateString();
            //    model.EndMissionDate = FormatDate(entity.EndDate);//entity.EndDate.ToShortDateString();
            //    model.GoalId = entity.Goal.Id;
            //    model.Id = entity.Id;
            //    model.TypeId = entity.Type.Id;
            //    model.Kind = entity.Kind;
            //    model.UserId = entity.User.Id;
            //    model.UserAllSum = FormatSum(entity.UserAllSum);
            //    model.UserAllSumAir = FormatSum(entity.UserSumAir);
            //    model.UserAllSumDaily = FormatSum(entity.UserSumDaily);
            //    model.UserAllSumResidence = FormatSum(entity.UserSumResidence);
            //    model.UserAllSumTrain = FormatSum(entity.UserSumTrain);
            //    model.DateCreated = entity.CreateDate.ToShortDateString();
            //    model.Version = entity.Version;
            //    model.UserSumCash = FormatSum(entity.UserSumCash);
            //    model.UserSumNotCash = FormatSum(entity.UserSumNotCash);

            //    model.IsResidencePaid = entity.IsResidencePaid;
            //    model.IsAirTicketsPaid = entity.IsAirTicketsPaid;
            //    model.IsTrainTicketsPaid = entity.IsTrainTicketsPaid;

            //    model.ResidenceRequestNumber = entity.ResidenceRequestNumber;
            //    model.AirTicketsRequestNumber = entity.AirTicketsRequestNumber;
            //    model.TrainTicketsRequestNumber = entity.TrainTicketsRequestNumber;
            //    model.SecretaryFio = entity.Secretary == null ? string.Empty : entity.Secretary.FullName;
            //    model.AirTicketType = entity.AirTicketType;
            //    model.TrainTicketType = entity.TrainTicketType;

            //    model.IsChiefApproveNeed = IsMissionOrderLong(entity);//entity.NeedToAcceptByChief;
            //    model.LongTermReason = entity.LongTermReason;
            //    model.DocumentNumber = entity.Number.ToString();

            //    MissionOrderTargetModel[] targets = entity.Targets.ToList().ConvertAll(x => new MissionOrderTargetModel
            //    {
            //        AirTicketTypeId = x.AirTicketType == null ? 0 : x.AirTicketType.Id,
            //        AirTicketTypeName = x.AirTicketType == null ? string.Empty : x.AirTicketType.Name,
            //        AllDaysCount = x.DaysCount.ToString(),
            //        City = x.City,
            //        Country = x.Country.Name,
            //        CountryId = x.Country.Id,
            //        DailyAllowanceId = x.DailyAllowance == null ? 0 : x.DailyAllowance.Id,
            //        DailyAllowanceName = x.DailyAllowance == null ? string.Empty : x.DailyAllowance.Name,
            //        DateFrom = x.BeginDate.ToShortDateString(),
            //        DateTo = x.EndDate.ToShortDateString(),
            //        Organization = x.Organization,
            //        ResidenceId = x.Residence == null ? 0 : x.Residence.Id,
            //        ResidenceName = x.Residence == null ? string.Empty : x.Residence.Name,
            //        TargetDaysCount = x.RealDaysCount.ToString(),
            //        TargetId = x.Id,
            //        TrainTicketTypeId = x.TrainTicketType == null ? 0 : x.TrainTicketType.Id,
            //        TrainTicketTypeName = x.TrainTicketType == null ? string.Empty : x.TrainTicketType.Name,
            //    }).ToArray();
            //    JsonList list = new JsonList { List = targets };
            //    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //    model.Targets = jsonSerializer.Serialize(list);
            //    if (entity.DeleteDate.HasValue)
            //        model.IsDeleted = true;
            //}
            //else
            //{
            //    JsonList list = new JsonList { List = new MissionOrderTargetModel[0] };
            //    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //    model.Targets = jsonSerializer.Serialize(list);
            //    model.DateCreated = DateTime.Today.ToShortDateString();
            //    LoadGraids(model, user.Grade.Value, entity, DateTime.Today);
            //    //model.IsEditable = true;
            //}
            //model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.MissionOrder);
            SetUserInfoModel(user, model);
            //SetFlagsState(id, user, entity, model);
            //SetStaticFields(model, entity);
            LoadDictionaries(model);
            //SetHiddenFields(model);
            return model;
        }
        protected void LoadDictionaries(HelpServiceRequestEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, RequestTypeEnum.HelpServiceRequest);
            
        }
        protected void SetUserInfoModel(User user, HelpUserInfoModel model)
        {
            if (user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
            if(user.Department != null)
            {
                model.Department2 = user.Department.Name;
                Department dep3 = DepartmentDao.GetParentDepartmentWithLevel(user.Department, 3);
                if (dep3 != null)
                    model.Department1 = dep3.Name;
                string managers = DepartmentDao.GetDepartmentManagers(user.Department.Id, true)
                                       .OrderByDescending(x => x.Level).
                                       Aggregate(string.Empty, (current, x) => 
                                       current + string.Format("{0} ({1}), ",x.FullName,x.Position == null ? "<не указана>": x.Position.Name));
                if (managers.Length >= 2)
                    managers = managers.Remove(managers.Length - 2);
                model.ManagerName = managers;
            }
        }

        #region Comments
        public CommentsModel GetCommentsModel(int id, RequestTypeEnum typeId)
        {
            CommentsModel commentModel = new CommentsModel
            {
                RequestId = id,
                RequestTypeId = (int)typeId,
                Comments = new List<RequestCommentModel>(),
                IsAddAvailable = id > 0,
            };
            if (id == 0)
                return commentModel;
            switch (typeId)
            {
                //case RequestTypeEnum.Appointment:
                //    Appointment entity = AppointmentDao.Load(id);
                //    if ((entity.Comments != null) && (entity.Comments.Count() > 0))
                //    {
                //        commentModel.Comments = entity.Comments.OrderBy(x => x.DateCreated).ToList().
                //            ConvertAll(x => new RequestCommentModel
                //            {
                //                Comment = x.Comment,
                //                CreatedDate = x.DateCreated.ToString(),
                //                Creator = x.User.FullName,
                //            });
                //    }
                //    break;
                //case RequestTypeEnum.AppointmentReport:
                //    AppointmentReport rep = AppointmentReportDao.Load(id);
                //    if ((rep.Comments != null) && (rep.Comments.Count() > 0))
                //    {
                //        commentModel.Comments = rep.Comments.OrderBy(x => x.DateCreated).ToList().
                //            ConvertAll(x => new RequestCommentModel
                //            {
                //                Comment = x.Comment,
                //                CreatedDate = x.DateCreated.ToString(),
                //                Creator = x.User.FullName,
                //            });
                //    }
                //    break;
                default:
                    throw new ValidationException(string.Format(AppointmentBl.StrInvalidCommentType, (int)typeId));

            }
            return commentModel;
        }
        public bool SaveComment(SaveCommentModel model, RequestTypeEnum type)
        {
            try
            {
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                switch (type)
                {
                    //case RequestTypeEnum.Appointment:
                    //    Appointment entity = AppointmentDao.Load(model.DocumentId);
                    //    AppointmentComment comment = new AppointmentComment
                    //    {
                    //        Comment = model.Comment,
                    //        Appointment = entity,
                    //        DateCreated = DateTime.Now,
                    //        User = user,
                    //    };
                    //    AppointmentCommentDao.MergeAndFlush(comment);
                    //    break;
                    //case RequestTypeEnum.AppointmentReport:
                    //    AppointmentReport rep = AppointmentReportDao.Load(model.DocumentId);
                    //    AppointmentReportComment comm = new AppointmentReportComment
                    //    {
                    //        Comment = model.Comment,
                    //        AppointmentReport = rep,
                    //        DateCreated = DateTime.Now,
                    //        User = user,
                    //    };
                    //    AppointmentReportCommentDao.MergeAndFlush(comm);
                    //    break;
                    default:
                        throw new ValidationException(string.Format(AppointmentBl.StrInvalidCommentType, (int)type));
                }
                return true;
            }
            catch (Exception ex)
            {
                //AppointmentCommentDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        #endregion
        #endregion
        #region Version
        public HelpVersionsListModel GetVersionsModel()
        {
            return new HelpVersionsListModel
                       {
                           IsAddAvailable = AuthenticationService.CurrentUser.UserRole == UserRole.Admin,
                           Versions = HelpVersionDao.LoadAllSortedByDate().ConvertAll(
                                x => new HelpVersionDto
                                         {
                                             Id = x.Id,
                                             Comment = x.Comment,
                                             ReleaseDate = x.ReleaseDate
                                         }
                           )
                       };
        }
        public HelpEditVersionModel GetEditVersionModel(int id)
        {
            HelpEditVersionModel model = new HelpEditVersionModel {Id = id};
            if (id == 0)
                model.ReleaseDate = DateTime.Today.ToShortDateString();
            else
            {
                HelpVersion version = HelpVersionDao.Load(id);
                model.ReleaseDate = version.ReleaseDate.ToShortDateString();
                model.Comment = version.Comment;
            }
            return model;
        }
        public bool SaveVersion(HelpSaveVersionModel model)
        {
            try
            {
                if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
                {
                    model.Error = StrCannotEditVersion;
                    return false;
                }
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                HelpVersion entity = new HelpVersion();                
                if(model.Id > 0)
                    entity = HelpVersionDao.Load(model.Id);
                entity.Comment = model.Comment;
                entity.DateCreated = DateTime.Now;
                entity.User = user;
                entity.ReleaseDate = model.ReleaseDate;
                HelpVersionDao.SaveAndFlush(entity);
                return true;
            }
            catch (Exception ex)
            {
                helpVersionDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        public bool DeleteVersion(DeleteAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotDeleteVersion;
                return false;
            }
            helpVersionDao.DeleteAndFlush(model.Id);
            return true;
        }
        #endregion
        #region Faq
        public HelpFaqListModel GetFaqModel()
        {
            return new HelpFaqListModel
            {
                IsAddAvailable = AuthenticationService.CurrentUser.UserRole == UserRole.Admin,
                Questions = HelpFaqDao.LoadAllSortedByQuestion().ConvertAll(
                     x => new HelpFaqDto
                     {
                         Id = x.Id,
                         Question = x.Question,
                         Answer = x.Answer
                     }
                )
            };
        }
        public HelpEditFaqModel GetEditQuestionModel(int id)
        {
            HelpEditFaqModel model = new HelpEditFaqModel { Id = id };
            if (id != 0)
            {
                HelpFaq entity = HelpFaqDao.Load(id);
                model.Question = entity.Question;
                model.Answer = entity.Answer;
            }
            return model;
        }
        public bool SaveFaq(HelpSaveFaqModel model)
        {
            try
            {
                if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
                {
                    model.Error = StrCannotEditFaq;
                    return false;
                }
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                HelpFaq entity = new HelpFaq();
                if (model.Id > 0)
                    entity = HelpFaqDao.Load(model.Id);
                entity.Question = model.Question;
                entity.Answer = model.Answer;
                entity.DateCreated = DateTime.Now;
                entity.User = user;
                HelpFaqDao.SaveAndFlush(entity);
                return true;
            }
            catch (Exception ex)
            {
                helpFaqDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        public bool DeleteFaq(DeleteAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotDeleteFaq;
                return false;
            }
            helpFaqDao.DeleteAndFlush(model.Id);
            return true;
        }
        #endregion
        #region Template
        public HelpTemplateListModel GetTemplateModel()
        {
            return new HelpTemplateListModel
            {
                IsAddAvailable = AuthenticationService.CurrentUser.UserRole == UserRole.Admin,
                Documents = RequestAttachmentDao.FindManyByRequestIdAndTypeId(0,RequestAttachmentTypeEnum.HelpTemplate).
                            ToList().ConvertAll(x => new HelpTemplateDto
                            {
                                Id = x.Id,
                                AttachmentId = x.Id,
                                Name = x.Description,
                            }).OrderBy(x => x.Name).ToList()
            };
        }
        public HelpTemplateEditModel GetTemplateEditModel(int id)
        {
            HelpTemplateEditModel model = new HelpTemplateEditModel { Id = id };
            if (id != 0)
            {
                RequestAttachment entity = RequestAttachmentDao.Load(id);
                model.Id = entity.Id;
                model.Name = entity.Description;
            }
            return model;
        }

        public bool SaveTemplate(SaveAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotEditFaq;
                return false;
            }
            RequestAttachment attach;
            if (model.Id == 0)
            {
                attach = new RequestAttachment
                    {
                        ContextType = RequestBl.GetFileContext(model.FileDto.FileName),
                        DateCreated = DateTime.Now,
                        Description = model.Description,
                        FileName = model.FileDto.FileName,
                        RequestId = 0,
                        RequestType = (int) model.EntityTypeId,
                        UncompressContext = model.FileDto.Context,
                        CreatorRole = RoleDao.Load((int) CurrentUser.UserRole)
                    };
            }
            else
            {
                attach = RequestAttachmentDao.Load(model.Id);
                attach.ContextType = RequestBl.GetFileContext(model.FileDto.FileName);
                attach.DateCreated = DateTime.Now;
                attach.Description = model.Description;
                attach.FileName = model.FileDto.FileName;
                attach.UncompressContext = model.FileDto.Context;
                attach.CreatorRole = RoleDao.Load((int) CurrentUser.UserRole);
            }
            RequestAttachmentDao.SaveAndFlush(attach);
            model.Id = attach.Id;
            return true;
        }
        public bool SaveTemplateName(SaveAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotEditFaq;
                return false;
            }
            RequestAttachment attach = RequestAttachmentDao.Load(model.Id);
            attach.DateCreated = DateTime.Now;
            attach.Description = model.Description;
            attach.CreatorRole = RoleDao.Load((int)CurrentUser.UserRole);
            RequestAttachmentDao.SaveFileNotChangeAndFlush(attach);
            return true;
        }

        #endregion
    }
}