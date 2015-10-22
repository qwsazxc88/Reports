using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class MailConfirmDao : DefaultDao<MailConfirm,Guid>, IMailConfirmDao
    {
        public MailConfirmDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
