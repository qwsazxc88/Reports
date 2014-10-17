using System;
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