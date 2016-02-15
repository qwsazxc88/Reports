using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core;
using Reports.Core.Services;
using Reports.Core.Dto;
using NHibernate;
using NHibernate.Transform;
namespace Reports.Core.Dao.Impl
{
    public class BugReportDao: DefaultDao<BugReport>, IBugReportDao
    {
        public BugReportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<BugReportDto> GetDocuments()
        {
            string sql = @"SELECT 
                            br.Id as 'Id',
                            br.CreateDate as 'CreateDate',
                            br.Summary as 'Summary',
                            br.Description as 'Description',
                            u.Name as 'UserName',
                            r.Name as 'UserRole'
                            FROM BugReport br
                            INNER JOIN USERS U ON BR.Id=U.Id
                            INNER JOIN Role r ON br.UserRole=r.Id";
            var query = Session.CreateSQLQuery(sql);
            query.AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("CreateDate", NHibernateUtil.DateTime)
                .AddScalar("Summary", NHibernateUtil.String)
                .AddScalar("Description", NHibernateUtil.String)
                .AddScalar("UserName", NHibernateUtil.String)
                .AddScalar("UserRole", NHibernateUtil.String);
            var data = query.SetResultTransformer(Transformers.AliasToBean(typeof(BugReportDto))).List<BugReportDto>();
            return data;
        }
    }
}
