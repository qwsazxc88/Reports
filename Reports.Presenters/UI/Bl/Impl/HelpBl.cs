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
    public class HelpBl : BaseBl, IHelpBl
    {

        public const string StrCannotEditVersion = "Вам запрещено редактирование информации о версии";
        public const string StrCannotDeleteVersion = "Вам запрещено удаление информации о версии";
        public const string StrException = "Исключение:";

        public const string StrCannotEditFaq = "Вам запрещено редактирование информации";
        public const string StrCannotDeleteFaq = "Вам запрещено удаление информации";
        public const string StrNoUser = "Не указан сотрудник для заявки на услугу";
        public const string StrUserNotManager = "Вы (пользователь {0}) не являетесь руководителем или сотрудником - создание заявки запрещено";
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
        protected IHelpServicePeriodDao helpServicePeriodDao;
        public IHelpServicePeriodDao HelpServicePeriodDao
        {
            get { return Validate.Dependency(helpServicePeriodDao); }
            set { helpServicePeriodDao = value; }
        }
        protected IHelpServiceProductionTimeDao helpServiceProductionTimeDao;
        public IHelpServiceProductionTimeDao HelpServiceProductionTimeDao
        {
            get { return Validate.Dependency(helpServiceProductionTimeDao); }
            set { helpServiceProductionTimeDao = value; }
        }
        protected IHelpServiceTransferMethodDao helpServiceTransferMethodDao;
        public IHelpServiceTransferMethodDao HelpServiceTransferMethodDao
        {
            get { return Validate.Dependency(helpServiceTransferMethodDao); }
            set { helpServiceTransferMethodDao = value; }
        }
        protected IHelpServiceTypeDao helpServiceTypeDao;
        public IHelpServiceTypeDao HelpServiceTypeDao
        {
            get { return Validate.Dependency(helpServiceTypeDao); }
            set { helpServiceTypeDao = value; }
        }
        protected IHelpServiceRequestDao helpServiceRequestDao;
        public IHelpServiceRequestDao HelpServiceRequestDao
        {
            get { return Validate.Dependency(helpServiceRequestDao); }
            set { helpServiceRequestDao = value; }
        }
        protected IHelpServiceRequestCommentDao helpServiceRequestCommentDao;
        public IHelpServiceRequestCommentDao HelpServiceRequestCommentDao
        {
            get { return Validate.Dependency(helpServiceRequestCommentDao); }
            set { helpServiceRequestCommentDao = value; }
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
            IUser current = AuthenticationService.CurrentUser;
            if (id == 0 && !userId.HasValue)
            {
                if (current.UserRole == UserRole.Employee)
                    userId = current.Id;
                else
                    throw new ValidationException(StrNoUser);
            }
            HelpServiceRequest entity = null;
            if (id != 0)
                entity = HelpServiceRequestDao.Load(id);
            HelpServiceRequestEditModel model = new HelpServiceRequestEditModel
            {
                Id = id,
                UserId = id == 0 ? userId.Value : entity.User.Id
            };
            User user = UserDao.Load(model.UserId);
            User currUser = UserDao.Load(current.Id);
            if(id == 0)
            {
                entity = new HelpServiceRequest
                             {
                                 User = user,
                                 Creator = currUser,
                                 CreateDate = DateTime.Now,
                                 EditDate = DateTime.Now,
                             };
            }
            else
            {
                model.ProductionTimeTypeId = entity.ProductionTime.Id;
                model.TransferMethodTypeId = entity.TransferMethod.Id;
                model.PeriodId = entity.Period == null? 0 :entity.Period.Id;
                model.Requirements = entity.Requirements;
                model.Version = entity.Version;
                model.DocumentNumber = entity.Number.ToString();
                model.DateCreated = FormatDate(entity.CreateDate);
                model.Creator = entity.Creator.FullName;
               
            }
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            SetFlagsState(id, currUser, entity, model);
            //SetStaticFields(model, entity);
            
            SetHiddenFields(model);
            return model;
        }
        protected void SetHiddenFields(HelpServiceRequestEditModel model)
        {
            model.TransferMethodTypeIdHidden = model.TransferMethodTypeId;
            model.ProductionTimeTypeIdHidden = model.ProductionTimeTypeIdHidden;
            model.TypeIdHidden = model.TypeId;
            model.PeriodIdHidden = model.PeriodId;

        }
        protected void SetFlagsState(int id, User current, HelpServiceRequest entity, HelpServiceRequestEditModel model)
        {
            UserRole currentRole = AuthenticationService.CurrentUser.UserRole;
            SetFlagsState(model, false);
            if (model.Id == 0)
            {
                if (currentRole != UserRole.Manager && currentRole != UserRole.Employee)
                    throw new ArgumentException(string.Format(StrUserNotManager, current.Id));
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                return;
            }
        }

        protected void SetFlagsState(HelpServiceRequestEditModel model, bool state)
        {
            model.IsBeginWorkAvailable = state;
            model.IsEditable = state;
            model.IsEndAvailable = state;
            model.IsEndWorkAvailable = state;
            model.IsSaveAvailable = state;
            model.IsSendAvailable = state;
            model.IsConsultantOutsourcingEditable = state;
            //model.IsServiceAttachmentVisible = state;
        }
        protected void LoadDictionaries(HelpServiceRequestEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, RequestTypeEnum.HelpServiceRequest);
            List<HelpServiceType> types = HelpServiceTypeDao.LoadAllSortedByOrder();
            model.Types = types.ConvertAll(x => new IdNameDto { Id = x.Id,Name = x.Name});
            model.ProductionTimeTypes = HelpServiceProductionTimeDao.LoadAllSortedByOrder().
                ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            model.TransferMethodTypes = HelpServiceTransferMethodDao.LoadAllSortedByOrder().
               ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            if(model.TypeId == 0)
                model.TypeId = types.First().Id;
            HelpServiceType type = types.Where(x => x.Id == model.TypeId).First();
            SetDistionariesFlag(model,type);
        }
        protected void SetDistionariesFlag(IHelpServiceDictionariesStates model, HelpServiceType type)
        {
            model.IsAttachmentVisible = type.IsAttachmentAvailable;
            model.IsPeriodVisible = type.IsPeriodAvailable;
            model.IsRequirementsVisible = type.IsRequirementsAvailable;
            if (type.IsPeriodAvailable)
            {
                List<HelpServicePeriod> periods = HelpServicePeriodDao.LoadForPeriodSortedByOrder(type.Id);
                model.Periods = periods.ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name});
                if(model.PeriodId == 0)
                {
                    int currMonth = DateTime.Today.Year * 100 + DateTime.Today.Month;
                    //if(type.Id == 4) //todo hardcode from DB
                    //    currMonth = DateTime.Today.Year*100 + DateTime.Today.Month;
                    //else
                    //    currMonth = DateTime.Today.Year*100 + 1;
                    HelpServicePeriod currPeriod = periods.OrderByDescending(x => x.PeriodMonth).
                        Where(x => x.PeriodMonth <= currMonth).FirstOrDefault();
                    if (currPeriod != null)
                        model.PeriodId = currPeriod.Id;
                }
            }
            else
                model.Periods = new List<IdNameDto>();
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
                                       .Where(x => x.Email != user.Email)
                                       .OrderByDescending(x => x.Level).
                                       Aggregate(string.Empty, (current, x) => 
                                       current + string.Format("{0} ({1}), ",x.FullName,x.Position == null ? "<не указана>": x.Position.Name));
                if (managers.Length >= 2)
                    managers = managers.Remove(managers.Length - 2);
                model.ManagerName = managers;
            }
        }


        public void GetDictionariesStates(int typeId,HelpServiceDictionariesStatesModel model)
        {
            HelpServiceType type = HelpServiceTypeDao.Load(typeId);
            SetDistionariesFlag(model, type);
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
                case RequestTypeEnum.HelpServiceRequest:
                    HelpServiceRequest entity = HelpServiceRequestDao.Load(id);
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
                    break;
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
                    case RequestTypeEnum.HelpServiceRequest:
                        HelpServiceRequest entity = HelpServiceRequestDao.Load(model.DocumentId);
                        HelpServiceRequestComment comment = new HelpServiceRequestComment
                        {
                            Comment = model.Comment,
                            Request = entity,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        HelpServiceRequestCommentDao.MergeAndFlush(comment);
                        break;
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