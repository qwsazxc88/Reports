using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dao;
using Reports.Core.Services;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Presenters.UI.ViewModel;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.Bl.Impl
{
    public class UserProfile: BaseBl, IUserProfile
    {
        #region Dao
        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }
        protected IPersonnelFileDao personnelFileDao;
        public IPersonnelFileDao PersonnelFileDao
        {
            get { return Validate.Dependency(personnelFileDao); }
            set { personnelFileDao = value; }
        }
        protected IDocumentPlaceDao documentPlaceDao;
        public IDocumentPlaceDao DocumentPlaceDao
        {
            get { return Validate.Dependency(documentPlaceDao); }
            set { documentPlaceDao = value; }
        }
        #endregion
        public void SendDocsTo(int PlaceId, int[] UserIds)
        {
            var currentUser = UserDao.Load(CurrentUser.Id);
            DocumentPlace place = null;
            if(PlaceId>0) place = DocumentPlaceDao.Load(PlaceId);
            var places = currentUser.Places.ToList();
            for (int i = 0; i < UserIds.Length; i++)
            {
                var doc = PersonnelFileDao.GetDocumentForUser(UserIds[i]);
                if (doc != null)
                {
                    if (((CurrentUser.UserRole&UserRole.ConsultantOutsourcing)>0||places.Any(x => x.Id == doc.CurrentPlaceId)) && !doc.IsArchive && doc.SendPlaceId == 0)
                    {
                        PersonnelFile send = place != null ? new PersonnelFile()
                        {
                            Place = place,
                            Sender = currentUser,
                            SendDate = DateTime.Now,
                            User = UserDao.Load(UserIds[i])
                        } :
                        new PersonnelFile() 
                        { 
                            Place = DocumentPlaceDao.Load(doc.CurrentPlaceId),
                            SendDate = DateTime.Now,
                            Sender = currentUser,
                            ReceiveDate = DateTime.Now,
                            Receiver = currentUser,
                            IsArchive = true,
                            ArchiveDate = DateTime.Now,
                            User = UserDao.Load(UserIds[i])
                        };
                        PersonnelFileDao.SaveAndFlush(send);                        
                    }
                }
            }
        }
        public void CancelSend(int[] UserIds)
        {
            var currentUser = UserDao.Load(CurrentUser.Id);
            var places = currentUser.Places.ToList();
            for (int i = 0; i < UserIds.Length; i++)
            {
                var doc = PersonnelFileDao.GetDocumentForUser(UserIds[i]);
                if (doc != null)
                {
                    if (((CurrentUser.UserRole&UserRole.ConsultantOutsourcing)>0||places.Any(x => x.Id == doc.CurrentPlaceId || x.Id==doc.SendPlaceId)) && !doc.IsArchive && doc.SendPlaceId != 0)
                    {
                        PersonnelFileDao.CancelSend(UserIds[i]);
                    }
                }
            }
        }
        public void ReceiveDocs(int[] UserIds)
        {
            var currentUser = UserDao.Load(CurrentUser.Id);
            var places = currentUser.Places.ToList();
            for (int i = 0; i < UserIds.Length; i++)
            {
                var doc = PersonnelFileDao.GetDocumentForUser(UserIds[i]);
                if (doc != null)
                {
                    if (((CurrentUser.UserRole&UserRole.ConsultantOutsourcing)>0||places.Any(x => x.Id == doc.SendPlaceId)) && !doc.IsArchive && doc.SendPlaceId != 0)
                    {
                        PersonnelFileDao.ReceiveSend(UserIds[i],currentUser.Id);
                    }
                }
            }
        }
        public PersonnelFileViewModel GetListModel()
        {
            PersonnelFileViewModel model = new PersonnelFileViewModel();
            var places = DocumentPlaceDao.LoadAll();
            model.Cities = places.Select(x => new IdNameDto { Name = x.Name, Id = x.Id }).ToList();
            return model;
        }
        public void SendEmailToAll()
        {
            var users = UserDao.GetUsersWhoMailNeeded();
            foreach (var user in users)
            {                
                    this.SendEmailsWithDocuments(user.Id);
            }
        }
        public void  SendEmailsWithDocuments(int userId)
        {
            var user = UserDao.Load(userId);
            if (String.IsNullOrEmpty(user.Email)) return;
            string Message = string.Empty;
            var roles = UserDao.GetAllUserRoles(userId);
            
            
            foreach (var role in roles)
            {
                var docs = UserDao.GetAllUserDocs(role.UserId, role.RoleId);
                if(docs!=null && docs.Any())
                {
                    if (String.IsNullOrEmpty(Message)) Message = "<html><head><style>body{font-family: Arial;}table {border-radius:5px;border:1px solid #E9F2F2;}td{border-radius:2px;}tr.odd {  background-color: #f9f9f9;}tr.even {  background-color: #E9F2F2;}</style><meta charset='UTF-8'/></head><body>";                    
                    Message += "<h2>Для роли " + ReportRoleConstants.RoleName[role.RoleId]+"</h2>";
                    Message += "<table>";
                    int i = 0;
                    foreach (var doc in docs)
                    {
                        i++;
                        Message += String.Format("<tr class='{0}'><td>{1}</td><td><a href='{2}'>{3}</a></td><td>{4}</td></tr>",i%2==0?"odd":"even",i,doc.ShortLink,doc.Name,doc.Department3Name);
                    }
                    Message += "</table>";
                }
            }
            if (string.IsNullOrEmpty(Message)) return;
            Message += "</body></html>";
            MailList mailmessage = new MailList();
            mailmessage.To = user;
            mailmessage.MailSubject="Кадровый портал. Заявки за " + DateTime.Now.ToShortDateString();
            mailmessage.MailText=Message;
            MailListDao.SaveAndFlush(mailmessage);
        }
        public IList<PersonnelFileDto> GetPersonnelFileDocuments(int depId)
        {
            var docs = PersonnelFileDao.GetDocuments(depId);
            SetIsEditable(docs);
            return docs;
        }
        public IList<PersonnelFileDto> GetPersonnelFileDocuments(string name)
        {
            var docs = PersonnelFileDao.GetDocuments(name);
            SetIsEditable(docs);
            return docs;
        }
        private void SetIsEditable(IList<PersonnelFileDto> docs)
        {
            var user = UserDao.Load(CurrentUser.Id);
            var places = user.Places;
            foreach (var doc in docs)
            {
                doc.IsEditable = !doc.IsArchive &&  ( (CurrentUser.UserRole&UserRole.ConsultantOutsourcing)>0 || places.Any(x => x.Id == doc.CurrentPlaceId || x.Id == doc.SendPlaceId) );
            }
        }
    }
}
