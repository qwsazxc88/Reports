using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Services;
using Reports.Core.Domain;
using Reports.Core.Dto;
using NHibernate;
using NHibernate.Transform;
namespace Reports.Core.Dao.Impl
{
    public class PersonnelFileDao: DefaultDao<PersonnelFile>, IPersonnelFileDao
    {
        public PersonnelFileDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<PersonnelFileDto> GetDocuments(int depId)
        {
            string sql = "SELECT * FROM vwPersonnelFiles where DepartmentId=" + depId;
            var query = CreateQuery(sql);
            return query.SetResultTransformer(Transformers.AliasToBean<PersonnelFileDto>()).List<PersonnelFileDto>();
        }
        public PersonnelFileDto GetDocumentForUser(int userid)
        {
            string sql = "SELECT top 1 * FROM vwPersonnelFiles where Id=" + userid;
            var query = CreateQuery(sql);
            return query.SetResultTransformer(Transformers.AliasToBean<PersonnelFileDto>()).UniqueResult<PersonnelFileDto>();
        }
        public IList<PersonnelFileDto> GetDocuments(string name)
        {
            string sql = "SELECT * FROM vwPersonnelFiles where Name like '" + name+"%'";
            var query = CreateQuery(sql);
            return query.SetResultTransformer(Transformers.AliasToBean<PersonnelFileDto>()).List<PersonnelFileDto>();
        }
        public void CancelSend(int UserId)
        {
            string sql = "UPDATE PersonnelFile SET CancelDate=GETDATE() where ReceiverId is null and UserId=" + UserId;
            Session.CreateSQLQuery(sql).ExecuteUpdate();
        }
        public void ReceiveSend(int UserId, int CurrentId)
        {
            string sql = "UPDATE PersonnelFile SET ReceiveDate=GETDATE(), ReceiverId=" + CurrentId + " where ReceiverId is null and CancelDate is null and UserId=" + UserId;
            Session.CreateSQLQuery(sql).ExecuteUpdate();
        }
        private ISQLQuery CreateQuery(string sql)
        {
            var query = Session.CreateSQLQuery(sql)
                   .AddScalar("Name", NHibernateUtil.String)
                   .AddScalar("Id", NHibernateUtil.Int32)
                   .AddScalar("Receiver", NHibernateUtil.String)
                   .AddScalar("Code", NHibernateUtil.String)
                   .AddScalar("Position", NHibernateUtil.String)
                   .AddScalar("ReceiverId", NHibernateUtil.Int32)
                   .AddScalar("Sender", NHibernateUtil.String)
                   .AddScalar("SenderId", NHibernateUtil.Int32)
                   .AddScalar("CurrentPlace", NHibernateUtil.String)
                   .AddScalar("CurrentPlaceId", NHibernateUtil.Int32)
                   .AddScalar("SendPlace", NHibernateUtil.String)
                   .AddScalar("SendPlaceId", NHibernateUtil.Int32)
                   .AddScalar("SourcePlace", NHibernateUtil.String)
                   .AddScalar("SourcePlaceId", NHibernateUtil.Int32)
                   .AddScalar("Department", NHibernateUtil.String)
                   .AddScalar("DepartmentId", NHibernateUtil.Int32)
                   .AddScalar("SendDate", NHibernateUtil.DateTime)
                   .AddScalar("ReceiveDate", NHibernateUtil.DateTime)
                //.AddScalar("CancelDate",NHibernateUtil.DateTime)
                   .AddScalar("ArchiveDate", NHibernateUtil.DateTime)
                   .AddScalar("IsArchive", NHibernateUtil.Boolean);
            return query;
        }

    }
}
