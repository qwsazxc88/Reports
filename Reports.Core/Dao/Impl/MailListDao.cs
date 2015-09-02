using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using NHibernate.Linq;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class MailListDao:DefaultDao<MailList>, IMailListDao
    {
        public MailListDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<MailList> GetMails()
        {
            List<MailList> result = new List<MailList>();
            var query = Session.Query<MailList>().Where(x => !x.SendDate.HasValue);
            if (query != null)
                result = query.ToList();
            if (result != null && result.Any()) return result.Take(50).ToList();
                else return new List<MailList>();                 
        }
    }
}
