using System;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class HelpBl : BaseBl, IHelpBl
    {

        public const string StrCannotEditVersion = "Вам запрещено редактирование информации о версии";
        public const string StrCannotDeleteVersion = "Вам запрещено удаление информации о версии";
        public const string StrException = "Исключение:";
        #region DAOs
        protected IHelpVersionDao helpVersionDao;
        public IHelpVersionDao HelpVersionDao
        {
            get { return Validate.Dependency(helpVersionDao); }
            set { helpVersionDao = value; }
        }
        #endregion
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
    }
}