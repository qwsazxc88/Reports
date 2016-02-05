﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dao;
using Reports.Core.Services;
using Reports.Core;
using Reports.Core.Domain;
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
        #endregion
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
                    if (String.IsNullOrEmpty(Message)) Message = "<html><head><style>body{font-family: Arial;}table {border-radius:5px;border:1px solid #E9F2F2;-webkit-box-shadow: 0px 0px 12px 1px rgba(70, 70, 70, 0.55);-moz-box-shadow:    0px 0px 12px 1px rgba(70, 70, 70, 0.55);box-shadow:         0px 0px 12px 1px rgba(70, 70, 70, 0.55);}td{border-radius:2px;}tbody > tr:nth-of-type(odd) {  background-color: #f9f9f9;}tbody > tr:nth-of-type(even) {  background-color: #E9F2F2;}tbody > tr:hover {  background-color: #CBDFF5;}</style><meta charset='UTF-8'/></head><body>";                    
                    Message += "<h2>Для роли " + ReportRoleConstants.RoleName[role.RoleId]+"</h2>";
                    Message += "<table>";
                    int i = 0;
                    foreach (var doc in docs)
                    {
                        i++;
                        Message += String.Format("<tr><td>{0}</td><td><a href='{1}'>{2}</a></td><td>{3}</td></tr>",i,doc.ShortLink,doc.Name,doc.Department3Name);
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
    }
}
